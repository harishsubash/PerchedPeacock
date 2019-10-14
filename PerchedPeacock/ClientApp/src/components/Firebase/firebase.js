import app from 'firebase/app';
import 'firebase/auth';

const config = {
  apiKey: "AIzaSyDYzWBJVSHHc5fLbRjiDP-62xBH3Jnj0So",
  authDomain: "perchedpeacock.firebaseapp.com",
  databaseURL: "https://perchedpeacock.firebaseio.com",
  projectId: "perchedpeacock",
  storageBucket: "perchedpeacock.appspot.com",
  messagingSenderId: "74522441798",
};

class Firebase {
  constructor() {
    app.initializeApp(config);

    this.auth = app.auth();
  }

  // *** Auth API ***

  doCreateUserWithEmailAndPassword = (email, password) =>
    this.auth.createUserWithEmailAndPassword(email, password);

  doSignInWithEmailAndPassword = (email, password) =>
    this.auth.signInWithEmailAndPassword(email, password);

  doSignOut = () => this.auth.signOut();

  doPasswordReset = email => this.auth.sendPasswordResetEmail(email);

  doPasswordUpdate = password =>
    this.auth.currentUser.updatePassword(password);
}

export default Firebase;
