import React from 'react';
import { AuthUserContext } from '../Session';
import { withAuthorization } from '../Session';

const BookingPage = () => (
  <AuthUserContext.Consumer>
    {authUser => (
      <div className ="py-5">
        Book Parking
      </div>
    )}
  </AuthUserContext.Consumer>
);

const authCondition = authUser => !!authUser;

export default withAuthorization(authCondition)(BookingPage);
