import React, { Component, useState } from "react";
import { withRouter } from 'react-router-dom';
import { withFirebase } from '../Firebase';
import { compose } from 'recompose';
import { useLocation, useHistory } from "react-router-dom";
import { AuthUserContext, withAuthorization } from "../Session";
import { Container, Row, Col } from "reactstrap";
import * as ROUTES from "../../constants/routes";

import {
  Form,
  FormGroup,
  Button,
  TextInput,
  DatePicker,
  DatePickerInput,
  TimePicker,
  TimePickerSelect,
  SelectItem
} from "carbon-components-react";
import Parking from "../Parking";

const BookingPage = () => (
  <AuthUserContext.Consumer>
    {authUser => (
      <div className="py-5">
          <h1>Booking</h1>
                    <ParkingLot />
                    <Container>
                        <BookingForm />
        </Container>
      </div>
    )}
  </AuthUserContext.Consumer>
);

const useLocationState = () => {
  return new URLSearchParams(useLocation().state);
};

const ParkingLot = () => {
  let name = useLocationState().get("name");
  let address = useLocationState().get("address");
  return (
    <div className="pt-5">
      <h5>{name}</h5>
      {address}
    </div>
  );
};


const INITIAL_STATE = {
  startdate: "",
  starttime: "",
  starttimeam: "AM",
  enddate: "",
  endtime: "",
  endtimeam: "AM",
    vehiclenumber: "",
    data: "",
    parkingCharge:'',

};

class BookingFormBase extends Component {
  constructor(props) {
    super(props);

    this.state = { ...INITIAL_STATE };
  }

  getFormattedDate = (date) => {
    if(date)
    {
      const dateObj = new Date(date);
      return dateObj.getMonth() + 1 + '/' + dateObj.getDate() + '/' + dateObj.getFullYear();
    }
    return "";
  }

  onStartDateChange = event => {
      this.setState({ ['startdate']: this.getFormattedDate(event[0]) });
      this.fetchCharges();
  };

  onStopDateChange = event => {
      this.setState({ ['enddate']: this.getFormattedDate(event[0]) });
      this.fetchCharges();
  };

  onChange = event => {
      this.setState({ [event.target.name]: event.target.value });
      this.fetchCharges();
  };

    async fetchCharges() {

        const { startdate,
            starttime,
            starttimeam,
            enddate,
            endtime,
            endtimeam,
            parkingCharge
        } = this.state;

        const isInvalid = (startdate === '' || starttime === '' || starttimeam === '' ||
            enddate === '' || endtime === '' || endtimeam === '');

        if (!isInvalid) {
            let parkingLotId = this.props.location.state.parkingLotId;
            let startdatetime = startdate + " " + starttime + " " + starttimeam;
            let enddatetime = enddate + " " + endtime + " " + endtimeam;

            let url = "api/ParkingLots/parkingRate?ParkingLotId=" +
                parkingLotId + "&StartDateTime=" + startdatetime + "&EndDateTime=" + enddatetime; 
            const response = await fetch(url);
            const data = await response.json();
            this.setState({ parkingCharge: data});
        }
    };

    onSubmit = event => {
    event.preventDefault();

    const {startdate,
      starttime,
      starttimeam,
      enddate,
      endtime,
      endtimeam,
      vehiclenumber,
    } = this.state;
        console.log(startdate);
        let parkingLotId = this.props.location.state.parkingLotId;
      console.log(parkingLotId);
      this.Book(parkingLotId, vehiclenumber);
  };

    async Book(parkingLotId, vehicleNumber) {
        var url ="api/ParkingLots/parkingSlot/book";
        const response = await fetch(url, {
            method: 'PUT',
            body: JSON.stringify({
                "parkingLotId": parkingLotId,
                "parkingSlotId": "00000000-0000-0000-0000-000000000000",
                "vehicleNumber": vehicleNumber
            }),
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        }).then(response => {
            return response.json()
        }).then(json => {
            console.log(json)
            this.setState({
                data: json
            });
            this.props.history.push(ROUTES.CONFIRMATION, json);
        })
    }
 

  render(){
    const {startdate,
      starttime,
      starttimeam,
      enddate,
      endtime,
      endtimeam,
        vehiclenumber,
        parkingCharge
    } = this.state;

    const isInvalid = (startdate === '' || starttime === '' || starttimeam === '' ||  
     enddate === '' || endtime === '' ||  endtimeam === '' || vehiclenumber === '');

  return (
    <Form onSubmit={this.onSubmit} className="pt-2">
      <Row>
        <Col>
          <Row>
            <FormGroup className="pr-2" legendText="">
              <DatePicker
                dateFormat="m/d/Y"
                datePickerType="single"
                id="startdate"
                light={false}
                locale="en"
                onChange={this.onStartDateChange}
                //onClose={onStartDateChange}
                short={false}
                value={startdate}
              >
                <DatePickerInput
                  disabled={false}
                  iconDescription="Icon description"
                  id="startdate-input-id"
                  invalid={false}
                  invalidText="A valid value is required"
                  labelText="Start date"
                  onChange={this.onStartDateChange}
                  onClick={this.onStartDateChange}
                  placeholder="mm/dd/yyyy"
                  type="text"
                />
              </DatePicker>
            </FormGroup>
            <FormGroup legendText="">
              <TimePicker
                id="starttime"
                name="starttime"
                value={starttime}
                onChange={this.onChange}
                labelText="select a time"
              >
                <TimePickerSelect
                  id="starttimeam"
                  name="starttimeam"
                  value={starttimeam}
                  onChange={this.onChange}
                  labelText=""
                >
                  <SelectItem value="AM" text="AM" />
                  <SelectItem value="PM" text="PM" />
                </TimePickerSelect>
              </TimePicker>
            </FormGroup>
          </Row>
          <Row>
            <FormGroup className="pr-2" legendText="">
            <DatePicker
                dateFormat="m/d/Y"
                datePickerType="single"
                id="stopdate"
                light={false}
                locale="en"
                onChange={this.onStopDateChange}
                //onClose={onStopDateChange}
                short={false}
                value={enddate}
              >
                <DatePickerInput
                  disabled={false}
                  iconDescription="Icon description"
                  id="stopdate-input-id"
                  invalid={false}
                  invalidText="A valid value is required"
                  labelText="Stop date"
                  onChange={this.onStopDateChange}
                  onClick={this.onStopDateChange}
                  placeholder="mm/dd/yyyy"
                  type="text"
                />
              </DatePicker>
            </FormGroup>
            <FormGroup legendText="">
              <TimePicker
                id="endtime"
                name="endtime"
                value={endtime}
                onChange={this.onChange}
                labelText="select a time"
              >
                <TimePickerSelect
                  id="endtimeam"
                  name="endtimeam"
                  value={endtimeam}
                  onChange={this.onChange}
                  labelText=""
                >
                  <SelectItem value="AM" text="AM" />
                  <SelectItem value="PM" text="PM" />
                </TimePickerSelect>
              </TimePicker>
            </FormGroup>
          </Row>
          <Row>
            <TextInput
              id="vehiclenumber"
              name="vehiclenumber"
              value={vehiclenumber}
              onChange={this.onChange}
              type="text"
              labelText="Vehicle registeration number"
              placeholder="Enter Vehicle Registeration Number (no spaces)"
            />
          </Row>
          <Row>
            <div className="py-3">
              Estimate parking charges for the selected duration is{" "}
                          <span className="font-weight-bolder">₹{parkingCharge}</span>
            </div>
          </Row>
          <Row>
            <Button disabled={isInvalid} type="submit">
              Submit
            </Button>
          </Row>
        </Col>
        <Col className="p-0 m-0 mt-4" />
      </Row>
    </Form>
  );
}};

const authCondition = authUser => !!authUser;

const BookingForm = compose(
  withRouter,
  withFirebase,
)(BookingFormBase);

export default withAuthorization(authCondition)(BookingPage);;

export { BookingForm };
