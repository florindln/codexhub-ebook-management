import React, { useEffect } from "react";
import { useState } from "react";
import { Button, Col, Container, Form, Modal, Row } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import { DeleteUser, EditUser, GetUserById } from "services/APIService";
import AuthService from "services/AuthService";

function ProfilePage() {
  const [user, setUser] = useState({
    email: "a",
    firstName: "a",
    id: "58fb2426-a865-4302-824b-991ce67f5193",
    interests: ["a", "b"],
    password: "a",
    lastName: "a",
    role: "User",
  });
  const [readOnlySetting, setReadOnlySetting] = useState(true);
  const [modalShow, setModalShow] = useState(false);
  const navigate = useNavigate();

  const handleModalClose = () => setModalShow(false);
  const handleModalShow = () => setModalShow(true);

  const deleteProfile = () => {
    DeleteUser(user.id).then((response) => {
      AuthService.logout();
      navigate("/");
      document.location.reload();
    });
  };

  const handleChange = (event) => {
    const name = event.target.name;
    const value = event.target.value;
    setUser((values) => ({
      ...values,
      [name]: value,
    }));
  };

  const handleInterestsChange = (event) => {
    let interests = event.target.value;
    interests = interests.split(",");
    setUser((values) => ({
      ...values,
      interests: interests,
    }));
    console.log(interests);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    console.log(user);
    EditUser(user.id, user).then(() => {
      setReadOnlySetting(true);
    });
  };

  useEffect(() => {
    const userId = AuthService.getCurrentUser().id;
    GetUserById(userId).then((user) => {
      // console.log(user.data);
      setUser(user.data);
    });
  }, []);

  return (
    <div>
      <Container className="mb-4">
        <Row>
          <Col md={2}></Col>
          <Col md={8}>
            <h2 className="my-3">My Profile</h2>
            <Form
              onSubmit={handleSubmit}
              className="mt-5 border border-dark rounded px-3 py-3"
            >
              <Form.Group
                as={Row}
                className="mb-3"
                controlId="formHorizontalFirstName"
              >
                <Form.Label column sm={2}>
                  FirstName
                </Form.Label>
                <Col sm={10}>
                  {readOnlySetting ? (
                    <Form.Control
                      value={user.firstName}
                      placeholder="FirstName"
                      readOnly
                    />
                  ) : (
                    <Form.Control
                      value={user.firstName}
                      placeholder="FirstName"
                      name="firstName"
                      onChange={handleChange}
                    />
                  )}
                </Col>
              </Form.Group>
              <Form.Group
                as={Row}
                className="mb-3"
                controlId="formHorizontalLastName"
              >
                <Form.Label column sm={2}>
                  LastName
                </Form.Label>
                <Col sm={10}>
                  {readOnlySetting ? (
                    <Form.Control
                      value={user.lastName}
                      placeholder="Last Name"
                      readOnly
                    />
                  ) : (
                    <Form.Control
                      value={user.lastName}
                      placeholder="Last Name"
                      name="lastName"
                      onChange={handleChange}
                    />
                  )}
                </Col>
              </Form.Group>
              <Form.Group
                as={Row}
                className="mb-3"
                controlId="formHorizontalEmail"
              >
                <Form.Label column sm={2}>
                  Email
                </Form.Label>
                <Col sm={10}>
                  {readOnlySetting ? (
                    <Form.Control
                      type="email"
                      value={user.email}
                      placeholder="Email"
                      readOnly
                    />
                  ) : (
                    <Form.Control
                      type="email"
                      value={user.email}
                      placeholder="Email"
                      name="email"
                      onChange={handleChange}
                    />
                  )}
                </Col>
              </Form.Group>

              <Form.Group
                as={Row}
                className="mb-3"
                controlId="formHorizontalPassword"
              >
                <Form.Label column sm={2}>
                  Password
                </Form.Label>
                <Col sm={10}>
                  {readOnlySetting ? (
                    <Form.Control
                      type="password"
                      value={user.password}
                      placeholder="Password"
                      readOnly
                    />
                  ) : (
                    <Form.Control
                      type="password"
                      value={user.password}
                      placeholder="Password"
                      name="password"
                      onChange={handleChange}
                      readOnly
                    />
                  )}
                </Col>
              </Form.Group>
              <Form.Group
                as={Row}
                className="mb-3"
                controlId="formHorizontalInterests"
              >
                <Form.Label column sm={2}>
                  Interests
                </Form.Label>
                <Col sm={10}>
                  {readOnlySetting ? (
                    <Form.Control
                      value={user.interests}
                      placeholder="Interests"
                      readOnly
                    />
                  ) : (
                    <Form.Control
                      value={user.interests}
                      placeholder="Interests"
                      name="interests"
                      onChange={handleInterestsChange}
                    />
                  )}
                </Col>
              </Form.Group>

              <Form.Group as={Row} className="mb-3">
                <Col sm={3}></Col>
                <Col>
                  {readOnlySetting ? (
                    <Button
                      onClick={(e) => {
                        setReadOnlySetting(false);
                        e.preventDefault();
                      }}
                    >
                      Edit Profile
                    </Button>
                  ) : (
                    <Button type="submit">Save</Button>
                  )}
                </Col>
                <Col>
                  {!readOnlySetting && (
                    <Button variant="danger" onClick={handleModalShow}>
                      Delete profile
                    </Button>
                  )}

                  <Modal show={modalShow} onHide={handleModalClose}>
                    <Modal.Header closeButton>
                      <Modal.Title>Delete profile?</Modal.Title>
                    </Modal.Header>
                    <Modal.Body>
                      Are you sure you want to permanently delete your profile?
                    </Modal.Body>
                    <Modal.Footer>
                      <Button variant="secondary" onClick={handleModalClose}>
                        Close
                      </Button>
                      <Button variant="primary" onClick={deleteProfile}>
                        Delete
                      </Button>
                    </Modal.Footer>
                  </Modal>
                </Col>
              </Form.Group>
            </Form>
          </Col>
          <Col md={2}></Col>
        </Row>
      </Container>
    </div>
  );
}

export default ProfilePage;
