import 'moment';
import moment from 'moment';

export const formatDateTime = (dateTime) =>
  dateTime ? moment(dateTime).format('DD/MM/yyy HH:mm:ss') : null;

export const formatElapsedSeconds = (elapsedSeconds) => {
  return new Date(elapsedSeconds * 1000).toISOString().substr(11, 8);
};

export const getDiffDurationInSeconds = (start, end) => {
  const startDate = moment(start);
  const endDate = moment(end);
  const diff = endDate.diff(startDate);
  const diffDuration = moment.duration(diff);

  return diffDuration.asSeconds();
};
