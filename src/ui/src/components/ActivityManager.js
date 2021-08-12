import { useState, useEffect } from 'react';

import { toast } from 'react-toastify';

import Activities from './Activities';
import AddActivity from './AddActivity';

const displayErrors = (errors) => {
  if (errors?.length > 0) {
    errors.forEach((error) => {
      toast.error(error.message, {
        position: 'top-center',
        autoClose: 5000,
        hideProgressBar: false,
        closeOnClick: true,
        pauseOnHover: false,
        draggable: false,
        progress: undefined,
      });
    });
  }
};

const baseUrl = 'https://localhost:5001';
const activityUrl = baseUrl + '/activity';

function ActivityManager() {
  const [activities, setActivities] = useState([]);
  useEffect(() => {
    const getActivities = async () => {
      const fetchActivities = async () => {
        try {
          const res = await fetch(activityUrl);
          const data = await res.json();
          return data;
        } catch (error) {
          alert(error);
          return;
        }
      };

      const activitiesResponse = await fetchActivities();
      if (activitiesResponse) {
        if (activitiesResponse.errors?.length > 0) {
          displayErrors(activitiesResponse.errors);
          return;
        }

        setActivities(activitiesResponse.result.activities);
      }
    };
    getActivities();
  }, []);

  const addActivity = async (newActivity) => {
    try {
      const res = await fetch(activityUrl, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newActivity),
      });

      const data = await res.json();

      if (!res.ok) {
        displayErrors(data.errors);
        return;
      }

      setActivities([...activities, data.result.activity]);
    } catch (error) {
      alert(error);
      return;
    }
  };

  const deleteActivity = async (id) => {
    try {
      const res = await fetch(`${activityUrl}/${id}`, {
        method: 'DELETE',
      });

      if (!res.ok) {
        const data = await res.json();
        displayErrors(data.errors);
        return;
      }

      setActivities(activities.filter((activity) => activity.id !== id));
    } catch (error) {
      alert(error);
      return;
    }
  };

  const startActivity = async (id) =>
    await performStartOrStopActivity('start', id);

  const stopActivity = async (id) =>
    await performStartOrStopActivity('stop', id);

  const performStartOrStopActivity = async (action, id) => {
    try {
      const res = await fetch(`${activityUrl}/${id}/${action}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      const data = await res.json();

      if (!res.ok) {
        displayErrors(data.errors);
        return;
      }

      const mergeArrayWithObject = (arr, obj) =>
        arr && arr.map((t) => (t.id === obj.id ? obj : t));

      setActivities(mergeArrayWithObject(activities, data.result.activity));
    } catch (error) {
      alert(error);
      return;
    }
  };

  return (
    <>
      <div className="d-grid gap-3">
        <div className="p-2 bg-light border">
          <AddActivity onAdd={addActivity} />
        </div>
        <div className="p-2 bg-light border">
          {activities.length > 0 ? (
            <Activities
              activities={activities}
              onStart={startActivity}
              onStop={stopActivity}
              onDelete={deleteActivity}
            />
          ) : (
            'It appears you have no activities! Click the button above to create one.'
          )}
        </div>
      </div>
    </>
  );
}

export default ActivityManager;
