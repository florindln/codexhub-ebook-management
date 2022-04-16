import React, { useState } from "react";
import { Button, Col, Container, Dropdown, Form, Row } from "react-bootstrap";
import BookCard from "./BookCard";
import BookFilterForm from "./BookFilterForm";
import SvgArrow from "./SvgArrow";

function Browse() {
  const [dropdownToggled, setDropdownToggled] = useState(false);
  const handleFilter = () => {
    setDropdownToggled(!dropdownToggled);
  };

  const books = [
    {
      title: "title",
      authors: ["author1", "author1"],
      description: "description",
      pageCount: 300,
      publishedDate: "publishedDate",
      thumbnailUrl: "https://picsum.photos/200",
    },
    {
      title: "title2",
      authors: ["author1", "author2"],
      description: "description2",
      pageCount: 300,
      publishedDate: "publishedDate",
      thumbnailUrl: "https://picsum.photos/200",
    },
  ];

  return (
    <div>
      <Container fluid>
        <Row>
          <Col md={3}></Col>
          <Col md={6} className="d-flex justify-content-start">
            <Container>
              <Row className="py-3">
                <h2>Browse all books</h2>
              </Row>
              <Row className="border border-dark rounded py-3">
                <Col onClick={handleFilter} xs={1}>
                  <div className="" style={{ width: "5rem" }}>
                    <SvgArrow right={dropdownToggled} />
                  </div>
                </Col>
                <Col
                  onClick={handleFilter}
                  xs={11}
                  className="d-flex justify-content-start align-items-center"
                >
                  <h2 className="ps-3"> Filter all books</h2>
                </Col>
                {dropdownToggled && <BookFilterForm />}
              </Row>
              <Row>
                {books.map((book, index) => (
                  <BookCard key={index} book={book} />
                ))}
                {/* <BookCard />; */}
              </Row>
            </Container>
          </Col>
          <Col md={3}></Col>
        </Row>
      </Container>
    </div>
  );
}

export default Browse;
