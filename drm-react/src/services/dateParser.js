export function DateFormatter(dateTime) {
    let date = new Date(dateTime);
    let day = date.getDate();
    let month = date.getUTCMonth() + 1;
    let year = date.getUTCFullYear();

    return `${day}-${month}-${year}`;
}

export function FullDateFormatter(dateTime) {
    let date = new Date(dateTime);
    let day = date.getDate();
    let month = date.getUTCMonth() + 1;
    let year = date.getUTCFullYear();
    let hour = date.getHours();
    let minutes = date.getMinutes();

    // kontrollo am apo pm
    let format = 'AM';
    if (hour > 12 && hour < 24) {
        format = 'PM';
        hour -= 12;
    }

    // mbush vlerat me 0
    if (hour < 10) {
        hour = '0' + hour.toString();
    }
    if (minutes < 10) {
        minutes = '0' + minutes.toString();
    }

    return `${day}-${month}-${year} ${hour}:${minutes} ${format}`;
}
