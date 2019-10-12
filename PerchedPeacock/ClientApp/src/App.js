import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Login from './components/Login';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import fire from './config/Fire';

import './App.scss'

export default class App extends Component {
    constructor() {
        super();
        this.state = ({
            user: null,
        });
        this.authListener = this.authListener.bind(this);
    }

    componentDidMount() {
        this.authListener();
    }

    authListener() {
        fire.auth().onAuthStateChanged((user) => {
            console.log(user);
            if (user) {
                this.setState({ user });
                localStorage.setItem('user', user.uid);
                user.getIdToken().then(function(idToken) {
                    localStorage.setItem('accessToken', idToken); // Nothing happens. No errors and the function not continues
                });
            } else {
                this.setState({ user: null });
                localStorage.removeItem('user');
            }
        });
    }

  render () {
    return (
        <Layout>
            <Route exact path='/' component={this.state.user ? Home: Login} />
            <Route path='/counter' component={Counter} />
            <Route path='/fetch-data' component={FetchData} />
      </Layout>
    );
  }
}
