import React from 'react';
import { ReactComponent as BuildingSvg } from "./../../assets/building.svg";
import {Button} from 'carbon-components-react';
import {Grid, Row, Col} from 'reactstrap'
import './index.scss';

const Parking = (props) => {
  const {
    parkingLot,
    index
  } = props

    const availabilityStatus = parkingLot.availableSlots == 0 ? 'Parking Full' : 'Available';

  return (
    <div class="shadow p-4 mb-4 bg-white rounded">
      <div class="d-flex">
        <div class="p-2"><BuildingSvg width="100" height="100"/></div>
        <div class="p-2 flex-fill">
          <ul>
            <li>
            <h4 className= "display-5">{parkingLot.name}</h4>
            </li>
                      <li className="py-3">
                          {parkingLot.address}
            </li>
          </ul>
          <Button onClick={props.handleBookParking.bind(parkingLot.parkingLotId, index)}>Book</Button>
        </div>
        <div class="p-2 flex-fill">
                  <h4 className="text-right px-5 pt-4 pb-3">₹ {parkingLot.hourlyParkingRate}/hr</h4>
                  <h6 className="text-right pr-5">{availabilityStatus}</h6>
        </div>
      </div>
    </div>
  );
}

export default Parking;

export {Parking}