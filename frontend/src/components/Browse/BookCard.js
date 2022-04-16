import React, { useState, useEffect } from "react";
import { Card, Col, Container, Row } from "react-bootstrap";

function BookCard(props) {
  const styles = {
    grid: {
      paddingLeft: 0,
      paddingRight: 0,
    },
    row: {
      marginLeft: 0,
      marginRight: 0,
    },
    col: {
      paddingLeft: 0,
      paddingRight: 0,
    },
  };
  useEffect(() => {
    setBook(props.book);
  });
  const [book, setBook] = useState({
    title: "title2",
    authors: ["author1", "author2"],
    description: "description2",
    pageCount: 300,
    initialPrice: 0,
    publishedDate: "publishedDate",
    thumbnailUrl: "https://picsum.photos/200",
  });
  return (
    <div>
      <Container className="my-3" style={styles.grid}>
        <Row style={styles.row}>
          <Col xs={4} className="d-flex justify-content-center">
            <img src={book.thumbnailUrl} alt="" />
          </Col>
          <Col xs={8} style={styles.col}>
            <Card>
              <Card.Body>
                <Card.Title>{book.title}</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">
                  {book.authors.map((author) => (
                    <span className="pe-2">{author}</span>
                  ))}
                </Card.Subtitle>
                <Card.Text>{book.description}</Card.Text>
                <Card.Text>
                  <span className="pe-2">{book.pageCount} pages |</span>
                  <span>Published on {book.publishedDate}</span>
                </Card.Text>
                <Card.Text>
                  <span className="pe-3">InitialPrice {book.initialPrice}</span>
                  <Card.Link href="#">Buy</Card.Link>
                  <Card.Link href="#">Another Link</Card.Link>
                </Card.Text>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default BookCard;
