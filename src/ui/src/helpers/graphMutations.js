import { gql } from '@apollo/client';

export const CREATE_ACTIVITY = gql`
  mutation createActivity($activity: CreateActivityRequest!) {
    createActivity(createActivityRequest: $activity) {
      id
      name
      laps {
        startDateTimeUtc
        endDateTimeUtc
      }
    }
  }
`;

export const DELETE_ACTIVITY = gql`
  mutation deleteActivity($activityId: ULong!) {
    deleteActivity(activityId: $activityId)
  }
`;

export const START_ACTIVITY = gql`
  mutation startActivity($activityId: ULong!) {
    startActivity(activityId: $activityId) {
      id
      name
      laps {
        startDateTimeUtc
        endDateTimeUtc
      }
    }
  }
`;

export const STOP_ACTIVITY = gql`
  mutation stopActivity($activityId: ULong!) {
    stopActivity(activityId: $activityId) {
      id
      name
      laps {
        startDateTimeUtc
        endDateTimeUtc
      }
    }
  }
`;
