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
  const [book, setBook] = useState({});
  return (
    <div>
      <Container style={styles.container}>
        <Row style={styles.row}>
          <Col xs={6} className="d-flex justify-content-center">
            <img src="https://picsum.photos/200" alt="" />
          </Col>
          <Col xs={6}>
            <Card>
              <Card.Body>
                <Card.Title>{book.title}</Card.Title>
                <Card.Subtitle className="mb-2 text-muted">b</Card.Subtitle>
                <Card.Text>{book.description}</Card.Text>
                <Card.Link href="#">Card Link</Card.Link>
                <Card.Link href="#">Another Link</Card.Link>
              </Card.Body>
            </Card>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default BookCard;
