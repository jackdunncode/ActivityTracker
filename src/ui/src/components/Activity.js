import './index.css';

import {
  formatDateTime,
  formatElapsedSeconds,
  getDiffDurationInSeconds,
} from '../helpers/dateTimeHelper';
import ElapsedDuration from './ElapsedDuration';

const activityStatuses = {
  notStarted: 'Activity not started',
  stillRunning: 'Activity still running',
};

const getFirstStartedLap = (activity) => {
  if (activity.laps.length > 0) {
    var started = activity.laps[0].startDateTimeUtc;
    return started;
  }

  return null;
};

const getFirstStartedElement = (activity) => {
  const firstStarted = getFirstStartedLap(activity);
  const text = firstStarted
    ? formatDateTime(firstStarted)
    : activityStatuses.notStarted;
  return <>{text}</>;
};

const getLastStoppedLap = (activity) => {
  if (activity.laps.length > 0) {
    var stopped = activity.laps[activity.laps.length - 1].endDateTimeUtc;
    return stopped;
  }

  return null;
};

const getLastStoppedElement = (activity) => {
  const firstStarted = getFirstStartedLap(activity);
  if (!firstStarted) return <>{activityStatuses.notStarted}</>;

  const allStopped = activity.laps.map((lap) => lap.endDateTimeUtc);

  if (allStopped.length === 0 || !allStopped[0])
    return <>{activityStatuses.stillRunning}</>;

  if (!allStopped[allStopped.length - 1]) allStopped.pop();

  const text = formatDateTime(allStopped[allStopped.length - 1]);
  return <>{text}</>;
};

const getTotalDurationElement = (activity) => {
  const firstStarted = getFirstStartedLap(activity);
  if (!firstStarted) return <>{activityStatuses.notStarted}</>;

  const lastStopped = getLastStoppedLap(activity);
  const total = activity.laps.reduce(
    (initialSeed, lap) =>
      getDiffDurationInSeconds(
        lap.startDateTimeUtc,
        lap.endDateTimeUtc ?? new Date()
      ) + initialSeed,
    0
  );

  if (firstStarted && lastStopped) {
    const totalElapsedSeconds = formatElapsedSeconds(total);
    return <>{totalElapsedSeconds}</>;
  }

  return <ElapsedDuration startAt={total} />;
};

function Activity({ activity, onStart, onStop, onDelete }) {
  return (
    <>
      <tr>
        <th scope="row">{activity.name}</th>
        <td>{getFirstStartedElement(activity)}</td>
        <td>{getLastStoppedElement(activity)}</td>
        <td>{getTotalDurationElement(activity)}</td>
        <td>
          <div
            className="btn-group"
            role="group"
            aria-label="Button group with nested dropdown"
          >
            <button
              type="button"
              className="btn btn-primary"
              onClick={() => onStart(activity.id)}
            >
              <i className="bi-play-circle"></i>
              Start
            </button>
            <button
              type="button"
              className="btn btn-danger"
              onClick={() => onStop(activity.id)}
            >
              <i className="bi-stop-circle"></i>
              Stop
            </button>

            <div className="btn-group" role="group">
              <button
                id="btnGroupDrop1"
                type="button"
                className="btn btn-secondary dropdown-toggle"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              ></button>
              <ul className="dropdown-menu" aria-labelledby="btnGroupDrop1">
                <li>
                  <button
                    className="dropdown-item"
                    onClick={() => onDelete(activity.id)}
                  >
                    <i className="bi-trash"></i>
                    Delete Activity
                  </button>
                </li>
              </ul>
            </div>
          </div>
        </td>
      </tr>
      {activity.laps.length > 0 ? (
        <tr>
          <td colSpan="5">
            <table className="table mb-0">
              <thead>
                <tr className="font-weight-normal">
                  <th></th>
                  <th scope="col">Started</th>
                  <th scope="col">Stopped</th>
                  <th scope="col">Duration</th>
                  <th></th>
                </tr>
              </thead>
              <tbody>
                {activity.laps.map((lap) => {
                  return (
                    <tr key={lap.id}>
                      <td></td>
                      <td>
                        {formatDateTime(lap.startDateTimeUtc) ??
                          activityStatuses.notStarted}
                      </td>
                      <td>
                        {formatDateTime(lap.endDateTimeUtc) ??
                          activityStatuses.stillRunning}
                      </td>
                      <td>
                        <ElapsedDuration
                          startTime={lap.startDateTimeUtc}
                          endTime={lap.endDateTimeUtc}
                        />
                      </td>
                      <td></td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </td>
        </tr>
      ) : (
        <tr>
          <td colSpan="5">Start the activity to begin recording laps.</td>
        </tr>
      )}
    </>
  );
}

export default Activity;
