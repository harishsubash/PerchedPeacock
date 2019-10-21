import React from 'react';
import { useLocation } from "react-router-dom";

const ConfirmationPage = () => (
    <div className="py-5">
        <h1>Confirmation</h1>
        <ConfirmationInfo/>
  </div>
);

const useLocationState = () => {
    return new URLSearchParams(useLocation().state);
};

const ConfirmationInfo = () => {
    let name = useLocationState().get("vehicleNumber");
    let slotNumber = useLocationState().get("slotNumber");
    return (
        <div className="pt-5">
            <h3>Thanks for booking the parking. Share this information at the time of entry</h3>
            <h5 className="pt-4">Vehicle number: {name} </h5>
            <h5>Slot number: {slotNumber} </h5>
        </div>
    );
};

export default ConfirmationPage;
