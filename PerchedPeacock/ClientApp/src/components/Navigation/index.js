import React , {Component} from 'react';
import { Link } from 'react-router-dom';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { ReactComponent as LogoSvg } from "./../../assets/logo.svg";

import SignOutButton from '../SignOut';
import * as ROUTES from '../../constants/routes';

import { AuthUserContext } from '../Session';

import './index.scss';

const INITIAL_STATE = {
  collapsed: true
};

class Navigation extends Component {
  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this)
    this.state = { ...INITIAL_STATE };
  }

  toggleNavbar = event => {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }
  
  static NavigationAuth = (collapsed) => {
    return (
      <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom-0 box-shadow navbar-color">
      <Container>
        <NavbarBrand tag={Link} to={ROUTES.LANDING}><LogoSvg style={{height:37}}/><span className="navbar-title">Perched Peacock</span></NavbarBrand>
        <NavbarToggler onClick={this.toggleNavbar} className="mr-2"></NavbarToggler>
        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
          <ul className="navbar-nav flex-grow">
            <NavItem><NavLink tag={Link} className="text-dark font-weight-bolder" to={ROUTES.ALLBOOKING}>View Bookings</NavLink></NavItem>
            <NavItem><NavLink tag={Link} className="text-dark font-weight-bolder" to={ROUTES.ACCOUNT}>Account</NavLink></NavItem>
            <SignOutButton/>
          </ul>
        </Collapse>
      </Container>
    </Navbar>
  )};

  static NavigationNonAuth = (collapsed) => {
    return ( 
      <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom-0 box-shadow navbar-color">
        <Container>
          <NavbarBrand tag={Link} to={ROUTES.LANDING}><LogoSvg style={{height:37}}/><span className="navbar-title">Perched Peacock</span></NavbarBrand>
          <NavbarToggler onClick={this.toggleNavbar} className="mr-2"></NavbarToggler>
          <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
            <ul className="navbar-nav flex-grow">
              <NavItem><NavLink tag={Link} className="text-dark font-weight-bolder" to={ROUTES.SIGN_IN}>Signin</NavLink></NavItem>
            </ul>
          </Collapse>
        </Container>
      </Navbar>
  )};

  render(){
    return(
    <div>
    <AuthUserContext.Consumer>
      {authUser =>
        authUser ? Navigation.NavigationAuth(this.state.collapsed): Navigation.NavigationNonAuth(this.state.collapsed)
      }
    </AuthUserContext.Consumer>
  </div>
    )};
}

export default Navigation;
