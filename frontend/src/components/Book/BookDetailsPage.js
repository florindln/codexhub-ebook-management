import { Rating } from "@mui/material";
import React, { useEffect, useState } from "react";
import { Container, Row, Col, Button } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { GetBookById } from "services/APIService";

function BookDetailsPage() {
  const { id } = useParams();
  const [book, setBook] = useState({
    id: "59444c98-9e37-4632-befb-f0598d9f4067",
    title: "Longarm Quilting Workbook",
    authors: ["Teresa Silva"],
    description: "Learn to Longarm with Confidence! Go",
    pageCount: 144,
    publishedDate: "2017-10-04T00:00:00",
    category: "Crafts & Hobbies",
    thumbnailURL:
      "http://books.google.com/books/content?id=BhZjDwAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api",
    initialPrice: 38,
  });
  const [rating, setRating] = useState(2);
  const [comment, setComment] = useState({
    commenterName: "",
    date: "",
    description: "",
  });

  useEffect(() => {
    // console.log(id);
    GetBookById(id).then((response) => setBook(response.data));
  }, []);
  return (
    <div>
      <Container fluid>
        <Row>
          <Col md={4}>
            <div className="sticky-top">
              <div className="d-flex justify-content-center">
                <img
                  //   width="75%"
                  height="100%"
                  src={book.thumbnailURL}
                  alt="BookPic"
                  className="my-3 w-75"
                />
              </div>
              <div className="d-flex justify-content-center">
                <Button className="w-75 my-3">
                  Buy for {book.initialPrice} EUR
                </Button>
              </div>{" "}
              <div className="d-flex justify-content-center">
                <Rating
                  name="simple-controlled"
                  value={rating}
                  onChange={(event, newValue) => {
                    setRating(newValue);
                    console.log(newValue);
                  }}
                  size="large"
                />
              </div>
              <div className="d-flex justify-content-center">
                Rate this book
              </div>
            </div>
          </Col>
          <Col md={7} className="ms-3">
            <Row className="my-3">
              <h2>{book.title}</h2>
            </Row>
            <Row className="my-3">
              <Col>
                <h6>
                  {book.authors.map((author, index) => (
                    <span className="me-3">{author}</span>
                  ))}
                </h6>
              </Col>
            </Row>
            <Row className="my-3">
              <Col>Ratings here</Col>
            </Row>
            <Row className="my-3">
              <Col>{book.description}</Col>
            </Row>
            <Row className="my-3">
              <Col>Pages: {book.pageCount}</Col>
              <Col>Published on {book.publishedDate}</Col>
            </Row>
            <Row>
              <Col>
                <h3>Community comments</h3>
              </Col>
            </Row>
          </Col>
        </Row>
        <Col md={1}></Col>
      </Container>
    </div>
  );
}

export default BookDetailsPage;
