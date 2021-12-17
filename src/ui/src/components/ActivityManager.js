import { useState, useEffect } from 'react';
import { useQuery, useMutation, gql } from '@apollo/client';

import { toast } from 'react-toastify';

import { GET_ACTIVITIES } from '../helpers/graphQueries';
import {
  CREATE_ACTIVITY,
  DELETE_ACTIVITY,
  START_ACTIVITY,
  STOP_ACTIVITY,
} from '../helpers/graphMutations';
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
  /* MUTATIONS/QUERIES */

  const {
    data: getActivitiesData,
    loading: getActivitiesLoading,
    error: getActivitiesError,
  } = useQuery(GET_ACTIVITIES);

  const [
    createActivityMutation,
    {
      data: createActivityData,
      loading: createActivityLoading,
      error: createActivityError,
    },
  ] = useMutation(CREATE_ACTIVITY, {
    refetchQueries: [GET_ACTIVITIES],
    onError: (error) => console.log(error),
    // update(cache, { data: { createActivityData } }) {
    //   cache.modify({
    //     fields: {
    //       activities(existingActivities = []) {
    //         const newActivityRef = cache.writeFragment({
    //           data: createActivityData,
    //           fragment: gql`
    //             fragment NewActivity on Activity {
    //               name
    //             }
    //           `,
    //         });
    //         return [...existingActivities, newActivityRef];
    //       },
    //     },
    //   });
    // },
  });

  const [
    deleteActivityMutation,
    {
      data: deleteActivityData,
      loading: deleteActivityLoading,
      error: deleteActivityError,
    },
  ] = useMutation(DELETE_ACTIVITY, {
    refetchQueries: [GET_ACTIVITIES],
    onError: (error) => console.log(error),
  });

  const [
    startActivityMutation,
    {
      data: startActivityData,
      loading: startActivityLoading,
      error: startActivityError,
    },
  ] = useMutation(START_ACTIVITY, {
    refetchQueries: [GET_ACTIVITIES],
    onError: (error) => console.log(error),
  });

  const [
    stopActivityMutation,
    {
      data: stopActivityData,
      loading: stopActivityLoading,
      error: stopActivityError,
    },
  ] = useMutation(STOP_ACTIVITY, {
    refetchQueries: [GET_ACTIVITIES],
    onError: (error) => {
      alert(error);
    },
  });

  /* EVENT HANDLERS */

  const addActivity = async (newActivity) => {
    createActivityMutation({
      variables: {
        activity: newActivity,
      },
    });
  };

  const deleteActivity = async (id) => {
    deleteActivityMutation({
      variables: {
        activityId: id,
      },
    });
  };

  const startActivity = async (id) => {
    startActivityMutation({
      variables: {
        activityId: id,
      },
    });
  };

  const stopActivity = async (id) => {
    stopActivityMutation({
      variables: {
        activityId: id,
      },
    });
  };

  if (getActivitiesLoading) return <p>Loading...</p>;
  if (getActivitiesError) return <p>Error :(</p>;

  return (
    <>
      <div className="d-grid gap-3">
        <div className="p-2 bg-light border">
          <AddActivity onAdd={addActivity} />
        </div>
        <div className="p-2 bg-light border">
          {getActivitiesData.activities.length > 0 ? (
            <Activities
              activities={getActivitiesData.activities}
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
