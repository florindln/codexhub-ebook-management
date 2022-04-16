import React from "react";
import { Form, Button, Col, Row } from "react-bootstrap";
import Multiselecter from "./Multiselecter";

function BookFilterForm() {
  return (
    <div>
      <Form className="px-3 py-3">
        {/* <Form.Group className="mb-3" controlId="formBasicEmail">
          <Form.Label>Email address</Form.Label>
          <Form.Control type="email" placeholder="Enter email" />
          <Form.Text className="text-muted">
            We'll never share your email with anyone else.
          </Form.Text>
        </Form.Group> */}
        <Form.Group className="mb-3">
          <Form.Label>Page count</Form.Label>
          <br />
          <Form.Check
            inline
            name="pageRadio"
            type="radio"
            label="<150"
          ></Form.Check>
          <Form.Check
            inline
            name="pageRadio"
            type="radio"
            label="150-300"
          ></Form.Check>
          <Form.Check
            inline
            name="pageRadio"
            type="radio"
            label=">300"
          ></Form.Check>
        </Form.Group>
        <Row className="mb-3">
          <h5>Published date</h5>
          <Form.Group as={Col} controlId="formGridCity">
            <Form.Label>From</Form.Label>
            <Form.Control />
          </Form.Group>
          <Form.Group as={Col} controlId="formGridZip">
            <Form.Label>To</Form.Label>
            <Form.Control />
          </Form.Group>
        </Row>
        <Form.Group>
          <Multiselecter />
        </Form.Group>

        {/* <Form.Group className="mb-3" controlId="formBasicCheckbox">
          <Form.Check type="checkbox" label="Check me out" />
        </Form.Group> */}
        <div className="d-flex justify-content-end">
          <Button variant="primary" type="submit">
            Submit
          </Button>
        </div>
      </Form>
    </div>
  );
}

export default BookFilterForm;
