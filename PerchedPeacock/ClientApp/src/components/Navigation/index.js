import React, { useState } from "react";
import { Link } from "react-router-dom";

import {
  Collapse,
  Container,
  Nav,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink
} from "reactstrap";
import { ReactComponent as LogoSvg } from "./../../assets/logo.svg";

import SignOutButton from "../SignOut";
import * as ROUTES from "../../constants/routes";

import { AuthUserContext } from "../Session";

import "./index.scss";

const Navigation = () => {
  return (
    <div className="pb-5">
      <AuthUserContext.Consumer>
        {authUser => (authUser ? <NavigationAuth /> : <NavigationNonAuth />)}
      </AuthUserContext.Consumer>
    </div>
  );
};

const NavigationNonAuth = () => {
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);

  return (
    <div>
      <Navbar light expand="md" className="navbar-color" fixed="top">
        <Container>
          <NavbarBrand tag={Link} to={ROUTES.LANDING}>
            <LogoSvg style={{ height: 37 }} />
            <span className="navbar-title">Perched Peacock</span>
          </NavbarBrand>
          <NavbarToggler onClick={toggle} />
          <Collapse isOpen={isOpen} navbar>
            <Nav className="ml-auto" navbar>
              <NavItem>
                <NavLink
                  tag={Link}
                  className="text-dark font-weight-bolder"
                  to={ROUTES.SIGN_IN}
                >
                  Signin
                </NavLink>
              </NavItem>
            </Nav>
          </Collapse>
        </Container>
      </Navbar>
    </div>
  );
};

const NavigationAuth = () => {
  const [isOpen, setIsOpen] = useState(false);
  const toggle = () => setIsOpen(!isOpen);
  return (
    <div className="navbar-color">
      <Container>
        <Navbar light expand="md">
          <NavbarBrand tag={Link} to={ROUTES.LANDING}>
            <LogoSvg style={{ height: 37 }} />
            <span className="navbar-title">Perched Peacock</span>
          </NavbarBrand>
          <NavbarToggler onClick={toggle} />
          <Collapse isOpen={isOpen} navbar>
                      <Nav className="ml-auto" navbar>
                          <NavItem>
                              <NavLink
                                  tag={Link}
                                  className="text-dark font-weight-bolder"
                                  to={ROUTES.VIEWBOOKING}
                              >
                                  View Bookings
                </NavLink>
                          </NavItem>
              <NavItem>
                <NavLink
                  tag={Link}
                  className="text-dark font-weight-bolder"
                  to={ROUTES.ACCOUNT}
                >
                  Account
                </NavLink>
              </NavItem>
              <SignOutButton />
            </Nav>
          </Collapse>
        </Navbar>
      </Container>
    </div>
  );
};

export default Navigation;
