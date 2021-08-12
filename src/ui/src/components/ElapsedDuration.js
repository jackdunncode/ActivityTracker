import { useElapsedTime } from 'use-elapsed-time';

import {
  formatElapsedSeconds,
  getDiffDurationInSeconds,
} from '../helpers/dateTimeHelper';

function ElapsedDuration({ startAt, startTime, endTime }) {
  const isPlaying = startTime && !endTime;

  if (!endTime) {
    endTime = new Date();
  }

  const props = {
    isPlaying: startAt ?? isPlaying,
    startAt: startAt ? startAt : getDiffDurationInSeconds(startTime, endTime),
    updateInterval: 1,
  };

  const { elapsedTime } = useElapsedTime(props);
  return <>{formatElapsedSeconds(elapsedTime)}</>;
}

export default ElapsedDuration;
