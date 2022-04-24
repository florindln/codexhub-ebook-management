import React from "react";
import { Card } from "react-bootstrap";

function SmallBookCard(props) {
  return (
    <div>
      <Card className="mx-2 my-2" style={{ width: "18rem" }}>
        <Card.Img variant="top" src={props.simplifiedBook.thumbnailURL} />
        <Card.Body>
          <Card.Title>{props.simplifiedBook.title}</Card.Title>
          <Card.Text>{props.simplifiedBook.category}</Card.Text>
          <Card.Text>
            {props.simplifiedBook.description.substring(0, 100)}...
          </Card.Text>
        </Card.Body>
      </Card>
    </div>
  );
}

export default SmallBookCard;
