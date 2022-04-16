import React from "react";
import ebooks from "../../images/ebooks.jpg";
import { Button, Col, Container, Row } from "react-bootstrap";

function Homepage() {
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
    leftPart: {
      backgroundColor: "#C0966A",
      height: "100%",
    },
  };

  return (
    <div>
      <Container fluid style={styles.grid}>
        <Row style={styles.row}>
          <Col md={6} style={styles.col}>
            <div
              className="d-flex justify-content-center align-items-center"
              style={styles.leftPart}
            >
              <Container>
                <Row>
                  <h1 className="text-center">Codexhub</h1>
                </Row>
                <Row>
                  <h2 className="text-center">Look for ebooks now!</h2>
                </Row>
                <Row>
                  <Col className="d-flex justify-content-end">
                    <Button variant="primary">Join</Button>
                  </Col>
                  <Col className="d-flex justify-content-start">
                    <Button variant="primary">Login</Button>
                  </Col>
                </Row>
              </Container>
            </div>
          </Col>
          <Col md={6} style={styles.col}>
            <img alt="rightpart" className="img-fluid" src={ebooks} />
          </Col>
        </Row>
        <Row style={{ minHeight: "150px" }}>
          <Col className="my-auto">
            <h3 className="text-center">Don't know what to read next?</h3>
            <h6 className="text-center">
              You’re in the right place. Tell us what titles or genres you’ve
              enjoyed in the past, and we’ll give you surprisingly insightful
              recommendations.
            </h6>
          </Col>
          <Col className="my-auto">
            <h3 className="text-center">Discover</h3>
            <h6 className="text-center">
              Try exploring books liked by others and more
            </h6>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default Homepage;
