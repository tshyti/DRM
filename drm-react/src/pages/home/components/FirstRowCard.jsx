import React from 'react';
import {
    Card,
    CardContent,
    Grid,
    Typography,
    makeStyles
} from '@material-ui/core';

import PropTypes from 'prop-types';

const useStyles = makeStyles({
    topNumber: {
        fontSize: '3rem',
        color: '#a1a1a1'
    },
    bottomText: {
        fontSize: '1rem',
        color: '#a1a1a1',
        paddingBottom: '5px'
    },
    noVerticalPadding: {
        paddingRight: 0,
        paddingTop: 0,
        '&:last-child': {
            paddingBottom: 0
        }
    },
    backgroundImageColor: {
        backgroundColor: '#f9fbfc',
        textAlign: 'center'
    },
    icon: {
        fontSize: '40px',
        position: 'relative',
        top: 'calc(50% - 23px)'
    }
});

export default function FirstRowCards({ text, number, Icon, iconColor }) {

    const styles = useStyles();
    const iconColorStyle = { color: iconColor };
    return (
        <Card>
            <CardContent className={styles.noVerticalPadding}>
                <Grid container>
                    <Grid item sm={8}>
                        <Grid container direction="column" spacing={0}>
                            <Grid item className={styles.noVerticalPadding}>
                                <Typography className={styles.topNumber}>{number}</Typography>
                            </Grid>
                            <Grid item className={styles.noVerticalPadding}>
                                <Typography className={styles.bottomText}>{text}</Typography>
                            </Grid>
                        </Grid>
                    </Grid>
                    <Grid item sm={4} className={styles.backgroundImageColor}>
                        <Icon style={{ color: iconColor }} className={styles.icon} />
                    </Grid>
                </Grid>
            </CardContent>
        </Card>
    );
}

FirstRowCards.propTypes = {
    text: PropTypes.string.isRequired,
    number: PropTypes.number.isRequired,
    Icon: PropTypes.object.isRequired,
    iconColor: PropTypes.string.isRequired
};
