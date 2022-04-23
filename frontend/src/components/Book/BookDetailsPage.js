import { Rating } from "@mui/material";
import Comments from "components/Comments/Comments";
import React, { useEffect, useState } from "react";
import { Container, Row, Col, Button } from "react-bootstrap";
import { useParams } from "react-router-dom";
import { GetBookById, GetRatingByBookId, RateBook } from "services/APIService";
import { DiscussionEmbed } from "disqus-react";
import AuthService from "services/AuthService";

function BookDetailsPage() {
  const { id } = useParams();
  const [book, setBook] = useState({
    id: "59444c98-9e37-4632-befb",
    title: "Test",
    authors: ["Teresa Silva", "Another"],
    description: "desc! Go",
    pageCount: 144,
    publishedDate: "2017-10-04T00:00:00",
    category: "Crafts & Hobbies",
    thumbnailURL:
      "http://books.google.com/books/content?id=BhZjDwAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api",
    initialPrice: 38,
  });
  const [averageRating, setAverageRating] = useState(1.5);
  const [rating, setRating] = useState(1.5);
  const [ratingInfo, setRatingInfo] = useState({
    averageRating: 0,
    ratingsCount: 0,
  });
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    GetBookById(id).then((response) => {
      setBook(response.data);
      setIsLoading(false);
      GetRatingByBookId(id).then((response) => {
        // console.log(response.data);
        setRatingInfo(response.data);
        // console.log(ratingInfo);
        setRating(response.data.averageRating);
        setAverageRating(response.data.averageRating);
      });
    });
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
                Average rating {averageRating.toString().substring(0, 3)} from{" "}
                {ratingInfo.ratingsCount} people
              </div>
              <div className="d-flex justify-content-center">
                <Rating
                  name="half-rating"
                  value={rating}
                  onChange={(event, newValue) => {
                    setRating(newValue);
                    if (AuthService.getUserRole() != "") {
                      RateBook({
                        BookId: book.id,
                        UserId: AuthService.getCurrentUser().id,
                        Rating: newValue,
                      });
                    }
                    // console.log(newValue);
                  }}
                  precision={0.5}
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
                    <span key={index} className="me-3">
                      {author}
                    </span>
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
                {/* <Comments
                  commentsUrl="http://localhost:3004/comments"
                  currentUserId="1"
                /> */}
                {/* {console.log(book.id, book.title)} */}
                {/* {!isLoading && console.log(book.id, book.title)} */}
                {!isLoading && (
                  <DiscussionEmbed
                    shortname="codexhubebook2"
                    config={{
                      url:
                        process.env.REACT_APP_CURRENT_URL + "/Books/" + book.id,
                      identifier: book.id,
                      title: book.title,
                    }}
                  />
                )}
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
