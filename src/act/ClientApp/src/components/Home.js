import React, {Component} from 'react';
import {Button, Form} from "react-bootstrap";

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = {
            forecasts: [],
            loading: true,
            formPayment: {
                name: "",
                email: "",
                amount: 0
            }
        };
        this.handleChangeName = this.handleChangeName.bind(this);
        this.handleChangeEmail = this.handleChangeEmail.bind(this);
        this.handleChangeAmount = this.handleChangeAmount.bind(this);
        this.makePayment = this.makePayment.bind(this);
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    static renderForecastsTable(forecasts) {
        return (
            <div>

                <hr/>
                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                    <tr>
                        <th>Id</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Amount</th>
                        <th>Payment At</th>
                    </tr>
                    </thead>
                    <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.id}>
                            <td>{forecast.id}</td>
                            <td>{forecast.customer.name}</td>
                            <td>{forecast.customer.email}</td>
                            <td>{forecast.amount}</td>
                            <td>{forecast.paymentAt}</td>
                        </tr>
                    )}
                    </tbody>
                </table>
            </div>
        );
    }

    async makePayment() {
        const model = this.state.formPayment;
        console.log((this.state));
        const requestOptions = {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(model)
        };
        const response = await fetch('Payment', requestOptions);
        await response.json();

        this.populateWeatherData();
    }

    handleChangeName(event) {
        this.setState({formPayment: {...this.state.formPayment, name: event.target.value}});
    }

    handleChangeEmail(event) {
        this.setState({formPayment: {...this.state.formPayment, email: event.target.value}});
    }

    handleChangeAmount(event) {
        this.setState({formPayment: {...this.state.formPayment, amount: parseFloat(event.target.value)}});
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Home.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel">Payment page </h1>
                <Form>
                    <Form.Group controlId="formPayment.name">
                        <Form.Label>Name</Form.Label>
                        <Form.Control type="text" placeholder="Name" onChange={this.handleChangeName}/>
                    </Form.Group>
                    <Form.Group controlId="formPayment.email">
                        <Form.Label>Email address</Form.Label>
                        <Form.Control type="email" placeholder="Enter email"
                                      onChange={this.handleChangeEmail}/>
                    </Form.Group>

                    <Form.Group controlId="formPayment.amount">
                        <Form.Label>Amount</Form.Label>
                        <Form.Control type="number" placeholder="Amount"
                                      onChange={this.handleChangeAmount}/>
                    </Form.Group>
                    <Button variant="primary" type="button" onClick={this.makePayment}>
                        Make Payment
                    </Button>
                </Form>
                <hr/>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        const response = await fetch('Payment');
        const data = await response.json();
        this.setState({forecasts: data.payload, loading: false});
    }
}
