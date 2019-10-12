
import firebase from 'firebase'

var firebaseConfig = {
    apiKey: "AIzaSyDYzWBJVSHHc5fLbRjiDP-62xBH3Jnj0So",
    authDomain: "perchedpeacock.firebaseapp.com",
    databaseURL: "https://perchedpeacock.firebaseio.com",
    projectId: "perchedpeacock",
    storageBucket: "perchedpeacock.appspot.com",
    messagingSenderId: "74522441798",
    appId: "1:74522441798:web:20962698b5401f4ddcf247"
  };

const fire = firebase.initializeApp(firebaseConfig);
export default fire;
