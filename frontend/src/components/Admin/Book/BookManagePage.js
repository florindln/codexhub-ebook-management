import React, { useEffect } from "react";
import { useState } from "react";
import { Form, Button, Container, Col, Row } from "react-bootstrap";
import { useLocation, useNavigate } from "react-router-dom";
import { EditBook, GetBookById } from "services/APIService";

function BookManagePage(props) {
  const { state } = useLocation();
  const navigate = useNavigate();
  try {
    var {
      id,
      title,
      authors,
      description,
      pageCount,
      publishedDate,
      category,
      thumbnailURL,
      initialPrice,
    } = state;
  } catch {
    var id =
      (title =
      authors =
      description =
      pageCount =
      publishedDate =
      category =
      thumbnailURL =
      initialPrice =
        "");
  }

  const [inputs, setInputs] = useState({
    id: id,
    title: title,
    authors: authors,
    description: description,
    pageCount: pageCount,
    publishedDate: publishedDate,
    category: category,
    thumbnailURL: thumbnailURL,
    initialPrice: initialPrice,
  });

  useEffect(() => {
    if (props.type === "edit") {
      GetBookById(id).then((book) => {
        setInputs(book.data);
      });
    }
  }, [id]);

  const handleChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    // console.log(name, value);
    setInputs((values) => ({
      ...values,
      [name]: value,
    }));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    // alert(inputs);
    // console.log(inputs);
    EditBook(inputs.id, inputs).then(() => {
      navigate(-1);
    });
  };

  return (
    <div>
      <Container>
        <Row>
          <Col md={2}></Col>
          <Col md={8}>
            {props.type === "edit" ? <h3>Edit book </h3> : <h3>Create book</h3>}
            <Form onSubmit={handleSubmit}>
              <Form.Group className="mb-3">
                <Form.Label>Title</Form.Label>
                <Form.Control
                  placeholder="Enter Title"
                  value={inputs.title}
                  name="title"
                  onChange={handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>authors</Form.Label>
                <Form.Control
                  placeholder="Enter authors"
                  value={inputs.authors}
                  name="authors"
                  onChange={handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>description</Form.Label>
                <Form.Control
                  as="textarea"
                  placeholder="Enter description"
                  value={inputs.description}
                  name="description"
                  onChange={handleChange}
                  rows={4}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>pageCount</Form.Label>
                <Form.Control
                  placeholder="Enter pageCount"
                  value={inputs.pageCount}
                  name="pageCount"
                  onChange={handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>publishedDate</Form.Label>
                <Form.Control
                  placeholder="Enter publishedDate"
                  value={inputs.publishedDate}
                  name="publishedDate"
                  onChange={handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>category</Form.Label>
                <Form.Control
                  placeholder="Enter category"
                  value={inputs.category}
                  name="category"
                  onChange={handleChange}
                />
              </Form.Group>{" "}
              <Form.Group className="mb-3">
                <Form.Label>thumbnailURL</Form.Label>
                <Form.Control
                  placeholder="Enter thumbnailURL"
                  value={inputs.thumbnailURL}
                  name="thumbnailURL"
                  onChange={handleChange}
                />
              </Form.Group>
              <Form.Group className="mb-3">
                <Form.Label>initialPrice</Form.Label>
                <Form.Control
                  placeholder="Enter initialPrice"
                  value={inputs.initialPrice}
                  name="initialPrice"
                  onChange={handleChange}
                />
              </Form.Group>
              <Button variant="primary" type="submit" className="mb-3">
                Submit
              </Button>
            </Form>
          </Col>
          <Col md={2}></Col>
        </Row>
      </Container>
    </div>
  );
}

export default BookManagePage;
