import { gql } from '@apollo/client';

export const GET_ACTIVITIES = gql`
  query {
    activities {
      id
      name
      laps {
        id
        startDateTimeUtc
        endDateTimeUtc
      }
    }
  }
`;
