import React from 'react';
import { Container } from 'reactstrap';
import { Search, Button } from 'carbon-components-react';

const Landing = () => (
    <div className="justify-content-center text-center pt-5 mt-5">
        <Container>
            <h3>Finding parking just got a lot simpler</h3>
            <div class="d-flex align-content-end justify-content-center mt-4">
                <div class="flex-fill p-2">
                    <Search></Search>
                </div>
                <div class="p-2">
                    <Button>Find Parkings</Button>
                </div>
            </div>
        </Container>
    </div>
);

export default Landing;
