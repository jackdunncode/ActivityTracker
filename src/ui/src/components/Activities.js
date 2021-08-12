import Activity from './Activity';

import './index.css';

function Activities({ activities, onStart, onStop, onDelete }) {
  return (
    <table className="table table-striped table-bordered">
      <thead>
        <tr className="table-header-blue">
          <th scope="col">Name</th>
          <th scope="col">First started</th>
          <th scope="col">Last stopped</th>
          <th scope="col">Total duration</th>
          <th scope="col">Actions</th>
        </tr>
      </thead>
      <tbody>
        {activities.map((activity) => (
          <Activity
            key={activity.id}
            activity={activity}
            onStart={onStart}
            onStop={onStop}
            onDelete={onDelete}
          />
        ))}
      </tbody>
    </table>
  );
}

export default Activities;
