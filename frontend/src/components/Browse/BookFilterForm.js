import React, { useState } from "react";
import { Form, Button, Col, Row } from "react-bootstrap";
import { FilterBooks } from "services/APIService";
import Multiselecter from "./Multiselecter";

function BookFilterForm(props) {
  const [filterOptions, setFilterOptions] = useState({
    title: "",
    pageCountMin: null,
    pageCountMax: null,
    publishedYearMin: null,
    publishedYearMax: null,
    genres: [],
  });

  const radioChanged = (event) => {
    switch (event.target.value) {
      case "150-radio":
        setFilterOptions((values) => ({
          ...values,
          pageCountMax: 150,
          pageCountMin: null,
        }));
        break;
      case "150-300-radio":
        setFilterOptions((values) => ({
          ...values,
          pageCountMin: 150,
          pageCountMax: 300,
        }));
        break;
      case "300-radio":
        setFilterOptions((values) => ({
          ...values,
          pageCountMin: 300,
          pageCountMax: null,
        }));
        break;
      default:
        console.log("radio button error no valid choice");
    }
  };

  const handleChangeNumber = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setFilterOptions((values) => ({
      ...values,
      [name]: parseInt(value),
    }));
  };

  const handleChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setFilterOptions((values) => ({
      ...values,
      [name]: value,
    }));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    // console.log(filterOptions);
    FilterBooks(filterOptions).then((books) => {
      // console.log(books.data);
      props.onHandleFilterBooks(books.data);
    });
  };

  const handleSelectedGenres = (items: Array) => {
    let itemValues = [];
    items.forEach((item) => {
      itemValues.push(item.value);
    });
    // console.log(itemValues);
    setFilterOptions((values) => ({
      ...values,
      genres: itemValues,
    }));
  };
  return (
    <div>
      <Form className="px-3 py-3" onSubmit={handleSubmit}>
        <Form.Group className="mb-3" onChange={radioChanged}>
          <Form.Label as="h5">Page count</Form.Label>
          <br />
          <Form.Check
            inline
            name="pageRadio"
            type="radio"
            value="150-radio"
            label="<150"
          ></Form.Check>
          <Form.Check
            inline
            name="pageRadio"
            type="radio"
            value="150-300-radio"
            label="150-300"
          ></Form.Check>
          <Form.Check
            inline
            name="pageRadio"
            type="radio"
            value="300-radio"
            label=">300"
          ></Form.Check>
        </Form.Group>
        <Row className="mb-3">
          <Form.Group className="mb-3" controlId="formGridCity">
            <Form.Label as="h5">Title</Form.Label>
            <Form.Control
              name="title"
              placeholder="Title"
              onChange={handleChange}
              value={filterOptions.title}
            />
          </Form.Group>
          <h5>Published year</h5>
          <Form.Group as={Col} controlId="formGridCity">
            <Form.Label>From</Form.Label>
            <Form.Control
              name="publishedYearMin"
              placeholder="Min"
              onChange={handleChangeNumber}
              value={
                filterOptions.publishedYearMin === null
                  ? ""
                  : filterOptions.publishedYearMin
              }
            />
          </Form.Group>
          <Form.Group as={Col} controlId="formGridZip">
            <Form.Label>To</Form.Label>
            <Form.Control
              name="publishedYearMax"
              placeholder="Max"
              onChange={handleChangeNumber}
              value={
                filterOptions.publishedYearMax === null
                  ? ""
                  : filterOptions.publishedYearMax
              }
            />
          </Form.Group>
        </Row>
        <Form.Group>
          <Multiselecter
            categories={props.categories}
            onHandleSelectedGenres={handleSelectedGenres}
          />
        </Form.Group>

        {/* <Form.Group className="mb-3" controlId="formBasicCheckbox">
          <Form.Check type="checkbox" label="Check me out" />
        </Form.Group> */}
        <div className="d-flex justify-content-end mt-3">
          <Button
            variant="warning"
            style={{ backgroundColor: "#b14512", color: "white" }}
            type="submit"
          >
            Submit
          </Button>
        </div>
      </Form>
    </div>
  );
}

export default BookFilterForm;
