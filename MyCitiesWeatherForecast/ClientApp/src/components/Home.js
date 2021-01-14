import React, { Component } from 'react';
import './site.css';
import $ from 'jquery'; 

export class Home extends Component {
  static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { forecasts: [], cityList: []};
        this.deleteCity = this.deleteCity.bind(this);
        this.deleteAllCities = this.deleteAllCities.bind(this);
        this.addCity = this.addCity.bind(this);
    }

    componentDidMount() {
        this.getData();
    }

    render() {
        return (
            <div className="row">
                <form method="post" onSubmit={this.addCity}>
                    <div className="modal fade bd-example-modal" id="exampleModalCenter" tabIndex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div className="modal-dialog modal modal-dialog-centered" role="document">
                            <div className="modal-content">
                                <div className="card product-card h-100 ">
                                    <div className="card-body">
                                        <div className="input-group mb-3 rounded" style={{ boxShadow: '0 0.2 rem 1 rem 0 rgba(0, 0, 0, 0.1)' }}>
                                            <select id="category" required className="bg-primary custom-select bg-light border-0" name="cityCode">
                                                <option value="" disabled defaultValue>City</option>
                                                {this.state.cityList.map(city =>
                                                    <option key={city.id} value={city.code}>{city.name}</option>
                                                )}
                                            </select>
                                        </div>

                                        <div className="input-group mb-3 border rounded">
                                            <div className="input-group rounded" style={{ boxShadow: '0 0.0 rem 0.0 rem 0 rgba(0, 0, 0, 0.1)' }}>
                                                <div className="input-group-prepend">
                                                    <span className="input-group-text border-0">Description</span>
                                                </div>
                                                <textarea required className="form-control border-0" name="Description" aria-label="Description" rows="6" maxLength="255"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div className="card-footer border-top-0 ">
                                        <button type="submit" className="text-white btn btn-primary border-0 pull-left">Create</button>
                                        <button type="button" className="close pull-right" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </form>

                <div className="col">
                    <h1 id="tabelLabel" >My city list</h1>
                    <p>Here you can see your city list.</p>
                </div>
                <div className="col buttonDiv">
                    <button className="btn btn-sm btn-primary buttonDivas" data-toggle="modal" data-target="#exampleModalCenter">Add new city</button>
                    <button className="btn btn-sm btn-primary buttonDivas" onClick={this.deleteAllCities}>Delete all cities</button>
                </div>

                <table className='table table-striped' aria-labelledby="tabelLabel">
                    <thead>
                        <tr>
                            <th>City name</th>
                            <th>Description</th>
                            <th>Max temperature</th>
                            <th>Min temperature</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.forecasts.map(forecast =>
                            <tr key={forecast.cityName}>
                                <td>{forecast.cityName}</td>
                                <td style={{ textAlign: 'left' }}>{forecast.description}</td>
                                <td>{forecast.maxTemp}</td>
                                <td>{forecast.minTemp}</td>
                                <td> <button id={forecast.id} className="btn btn-sm btn-primary" onClick={this.deleteCity}>Delete</button> </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }

    async getData() {
        const response = await fetch('city/mylist', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        });

        const cityList = await fetch('city', {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            }
        });

        const data = await response.json();
        const cityListData = await cityList.json();
        this.setState({ forecasts: data, cityList: cityListData});
    }

    async addCity(event) {
        event.preventDefault();
        const formData = new FormData(event.target);

        $(function () {
            window.$('#exampleModalCenter').modal('toggle');
        });

        const response = await fetch('city', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                code: formData.get('cityCode'),
                description: formData.get('Description')
            })
        });

        const data = await response.json();

        if (data === "Created") {
            window.location.reload();
        }
        else {
            alert(data);
        }
    }

    async deleteCity(event) {
        if (window.confirm('Are you sure you wish to delete this city?')) {
            event.preventDefault();
            let cityId = event.currentTarget.id;
            console.log(cityId);

            const result = await fetch('city/' + cityId, {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                }
            });

            const data = await result.json();
            if (data === "Deleted") {
                window.location.reload();
            }
            else {
                alert(data);
            }
        }
    }

    async deleteAllCities(event) {
        if (window.confirm('Are you sure you wish to delete everything?')) {
            event.preventDefault();

            const result = await fetch('city', {
                method: 'DELETE',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                }
            });

            const data = await result.json();
            if (data === "Deleted") {
                window.location.reload();
            }
            else {
                alert(data);
            }
        }
    }
}
