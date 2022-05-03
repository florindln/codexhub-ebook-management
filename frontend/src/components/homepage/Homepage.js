import React, { useEffect, useState } from "react";
import ebooks from "../../images/ebooks.jpg";
import { Button, CardGroup, Col, Container, Row } from "react-bootstrap";
import {
  GetRecommendationsByUserId,
  GetRandomRecommendations,
} from "services/APIService";
import AuthService from "services/AuthService";
import SmallBookCard from "./SmallBookCard";

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

  const defaultSimplifiedBooks = [
    {
      id: "76a7916b-4fb8-45d6-b7fb-4728237b3cbd",
      title: "Longarm Quilting Workbook",
      description: "Learn to Longarm ",
      initialPrice: 44.0,
      category: "Crafts & Hobbies",
      thumbnailURL:
        "http://books.google.com/books/content?id=xt_-qyFy2PIC&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api",
    },
    {
      id: "76a7916b-4fb8-45d6-b7fb-4728237b3cbd",
      title: "Longarm Quilting Workbook",
      description: "Learn to Longarm ",
      initialPrice: 44.0,
      category: "Crafts & Hobbies",
      thumbnailURL:
        "http://books.google.com/books/content?id=xt_-qyFy2PIC&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api",
    },
  ];

  const [simplifiedBooks, setSimplifiedBooks] = useState(
    defaultSimplifiedBooks
  );
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    let userId = AuthService.getCurrentUser().id;
    if (userId != "") {
      setIsLoggedIn(true);
      GetRecommendationsByUserId(userId, 4).then((recommendations) => {
        setSimplifiedBooks(recommendations.data);
      });
    } else {
      GetRandomRecommendations(4).then((recommendations) => {
        setSimplifiedBooks(recommendations.data);
      });
    }
  }, []);

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
                {!isLoggedIn && (
                  <Row>
                    <Col className="d-flex justify-content-end">
                      <Button variant="primary">Join</Button>
                    </Col>
                    <Col className="d-flex justify-content-start">
                      <Button variant="primary">Login</Button>
                    </Col>
                  </Row>
                )}
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
        <Row className="my-3">
          <Col>
            <div className="ms-5 ps-5">
              {isLoggedIn ? (
                <h4>Some personal recommendations based on your interests</h4>
              ) : (
                <h4>Random books you might like</h4>
              )}
            </div>
            <CardGroup className="py-2 justify-content-center">
              {simplifiedBooks.map((book, index) => (
                <SmallBookCard key={index} simplifiedBook={book} />
              ))}
            </CardGroup>
          </Col>
        </Row>
      </Container>
    </div>
  );
}

export default Homepage;
