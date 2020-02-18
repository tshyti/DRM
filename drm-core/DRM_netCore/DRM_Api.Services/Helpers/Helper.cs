using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace DRM_Api.Services.Helpers
{
	public static class Helper
	{
		#region Password hashing helper methods
		public static string ComputeHash(string originalString)
		{
			byte[] salt;
			byte[] buffer2;
			if (originalString == null)
			{
				throw new ArgumentNullException(nameof(originalString));
			}

			using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(originalString, 16, 1000))
			{
				salt = bytes.Salt;
				buffer2 = bytes.GetBytes(32);
			}
			byte[] dst = new byte[49];
			Buffer.BlockCopy(salt, 0, dst, 1, 16);
			Buffer.BlockCopy(buffer2, 0, dst, 17, 32);
			return Convert.ToBase64String(dst);
		}

		public static bool VerifyHash(string hashedString, string originalString)
		{
			byte[] buffer4;
			if (hashedString == null)
			{
				return false;
			}

			if (originalString == null)
			{
				throw new ArgumentNullException(nameof(originalString));
			}

			byte[] src = Convert.FromBase64String(hashedString);
			if (src.Length != 49 || src[0] != 0)
			{
				return false;
			}
			byte[] dst = new byte[16];
			Buffer.BlockCopy(src, 1, dst, 0, 16);
			byte[] buffer3 = new byte[32];
			Buffer.BlockCopy(src, 17, buffer3, 0, 32);
			using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(originalString, dst, 1000))
			{
				buffer4 = bytes.GetBytes(32);
			}

			return ByteArraysEqual(buffer3, buffer4);
		}

		private static bool ByteArraysEqual(byte[] src1, byte[] src2)
		{
			if (src1 == src2)
			{
				return true;
			}

			if (src1 == null || src2 == null || (src1.Length != src2.Length))
			{
				return false;
			}

			for (int i = 0; i < src1.Length; i++)
			{
				if (src1[i] != src2[i])
				{
					return false;
				}
			}

			return true;
		} 
		#endregion

		public static async Task<string> UploadFileToBlobAsync(string azureFileName, byte[] fileData, string fileMimeType, string accessKey, string containerName)
		{
				CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(accessKey);
				CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
				CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
				if (await cloudBlobContainer.CreateIfNotExistsAsync())
				{
					await cloudBlobContainer.SetPermissionsAsync(
						new BlobContainerPermissions{ PublicAccess = BlobContainerPublicAccessType.Blob});
				}

				if (!string.IsNullOrEmpty(azureFileName) && fileData != null)
				{
					CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(azureFileName);
					cloudBlockBlob.Properties.ContentType = fileMimeType;
					await cloudBlockBlob.UploadFromByteArrayAsync(fileData, 0, fileData.Length);
					return cloudBlockBlob.Uri.AbsoluteUri;
				}
				return String.Empty;
		}

		public static string GenerateFileName(string fileName)
		{
			string strFileName = String.Empty;
			strFileName = DateTime.Now.ToUniversalTime()
				              .ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToUniversalTime()
				              .ToString("yyyyMMdd\\THHmmssfff") + "_" + fileName;
			return strFileName;
		}

		public static async Task<byte[]> DownloadBlobFiles(string fileName, string accessKey, string containerName)
		{
			if (CloudStorageAccount.TryParse(accessKey, out CloudStorageAccount cloudStorageAccount))
			{
				CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
				CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);

				if (await cloudBlobContainer.ExistsAsync())
				{
					CloudBlockBlob blob = cloudBlobContainer.GetBlockBlobReference(fileName);
					using (MemoryStream ms = new MemoryStream())
					{
						await DownloadFileFromBlobParallel(blob, ms);
						List<byte> byteList = new List<byte>();
						int index = 0;
						int size = 1 * 1024 * 1024;
						long fileSize = ms.Length;
						ms.Position = 0;
						while (fileSize > 0)
						{
							int chunkSize = Math.Min(size, (int)fileSize);
							byte[] bytesInStream = new byte[chunkSize];
							ms.Read(bytesInStream, 0, chunkSize);
							index += chunkSize;
							fileSize -= chunkSize;
							byteList.AddRange(bytesInStream);
						}

						return byteList.ToArray();
					}
				}
				else
				{
					throw new BaseException(404, "File does not exist.");
				}
			}
			else
			{
				throw new BaseException(500, "Can't connect to server. Try again later.");
			}
		}

		private static async Task DownloadFileFromBlobParallel(CloudBlockBlob blob, Stream outputStream)
		{
			if (await blob.ExistsAsync())
			{
				// await file.DownloadToStreamAsync(ms);
				await blob.FetchAttributesAsync();
				int buferLength = 4 * 1024 * 1024; // 4 MB chunks
				Queue<KeyValuePair<long, long>> queue = new Queue<KeyValuePair<long, long>>();
				long blobRemainingLength = blob.Properties.Length;
				long index = 0;

				while (blobRemainingLength > 0)
				{
					long chunkLength = Math.Min(buferLength, blobRemainingLength);
					queue.Enqueue(new KeyValuePair<long, long>(index, chunkLength));
					index += chunkLength;
					blobRemainingLength -= chunkLength;
				}
				int i = 0;
				List<Task> tasks = new List<Task>();
				foreach (var item in queue)
				{
					tasks.Add(Task.Run(async () =>
					{
						using (var memoryStream = new MemoryStream())
						{
							await blob.DownloadRangeToStreamAsync(memoryStream, item.Key, item.Value);
							lock (outputStream)
							{
								outputStream.Position = item.Key;
								var bytes = memoryStream.ToArray();
								outputStream.Write(bytes, 0, bytes.Length);
							}
						}
					}));
					i++;
					if (i == 5) // Number of parallel chunks downloading at the same time
					{
						Task.WaitAll(tasks.ToArray());
						tasks.Clear();
						i = 0;
					}
				}
				Task.WaitAll(tasks.ToArray());
			}
		}
	}
}