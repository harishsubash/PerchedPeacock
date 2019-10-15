import React, {Component} from 'react';
import { withRouter } from 'react-router-dom';
import { withFirebase } from '../Firebase';
import { Container } from 'reactstrap';
import { Button, Search, Form } from 'carbon-components-react';
import {Parking} from '../Parking'
import './index.scss';
import * as ROUTES from '../../constants/routes';

const Landing = () => (
    <div className="pt-5 mt-3">
        <Container>
            <h2 className="text-center">Finding parking just got a lot simpler</h2>
            <LandingPageForm/>
        </Container>
    </div>
);

const INITIAL_STATE = {
    searchText: '',
    parkingLotData:'',
  };

class LandingPageFormBase extends Component {
    constructor(props) {
        super(props);

        this.state = { ...INITIAL_STATE };
    }

    onSubmit = event => {
        const { searchText } = this.state;
        this.populateParkingLot(searchText);
    };

    async populateParkingLot(searchText) {
        var address = searchText.split(',');
        var url = 'api/ParkingLots/location?City=' + address[0].trim() + '&Country=' + address[1].trim()
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ parkingLotData: data, loading: false });
    }

    onChange = event => {
        this.setState({ 'searchText': event.target.value });
    };

    handleBookParking = (index) => {
        if (index)
            this.props.history.push(ROUTES.BOOKING);
    }

    render() {
        const { parkingLotData } = this.state;

        return (
            <>
                <div class="d-flex align-content-end justify-content-center mt-4">
                    <Search
                        name="citySearch"
                        onChange={this.onChange}
                        type="text"
                        placeHolderText="Enter Bangalore, India"
                    />
                    <Button type="submit" onClick={this.onSubmit}>
                        Find Parking
                </Button>
                </div>
                {parkingLotData &&
                    <div className="mt-5 pt-5">
                        <h3 className="display-5">Parkings in your city</h3>
                        <div className="py-2">
                        {parkingLotData.parkingLots.map((parkingLot, index) => {
                                return (
                                    <Parking
                                        parkingLot={parkingLot}
                                        handleBookParking={this.handleBookParking}
                                        index={index}
                                    />
                                )
                            })}
                        </div>
                    </div>
                }

            </>
        );
    }
}

  const LandingPageForm = withRouter(withFirebase(LandingPageFormBase));

export default Landing;

export{LandingPageForm}
