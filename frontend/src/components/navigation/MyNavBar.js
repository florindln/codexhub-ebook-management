import React, { useEffect, useState } from "react";
import {
  Navbar,
  Container,
  Nav,
  NavDropdown,
  Form,
  InputGroup,
} from "react-bootstrap";
import CodexHubLogo from "../../images/codexhubLogo.png";
import { styled, alpha } from "@mui/material/styles";
import SearchIcon from "@mui/icons-material/Search";
import InputBase from "@mui/material/InputBase";
import { useNavigate } from "react-router-dom";
import { Button } from "@mui/material/";
import { Link } from "react-router-dom";
import "./MyNavBar.css";
import AuthService from "services/AuthService";

const StyledInputBase = styled(InputBase)(({ theme }) => ({
  color: "inherit",
  "& .MuiInputBase-input": {
    padding: theme.spacing(1, 1, 1, 0),
    // vertical padding + font size from searchIcon
    paddingLeft: `calc(1em + ${theme.spacing(4)})`,
    transition: theme.transitions.create("width"),
    width: "100%",
    [theme.breakpoints.up("sm")]: {
      width: "12ch",
      "&:focus": {
        width: "20ch",
      },
    },
  },
}));

function MyNavBar() {
  let navigate = useNavigate();

  function GoHome() {
    navigate("/information");
  }

  const [showDropdown, setShowDropdown] = useState(false);
  const [permissionRole, setPermissionRole] = useState("");

  useEffect(() => {
    const role = AuthService.getUserRole();
    setPermissionRole(role);
    // console.log("role: " + role);
    console.log("permissionRole: " + permissionRole);
  });

  const showDropdownFunc = (e) => {
    setShowDropdown(true);
  };

  return (
    <div>
      <Navbar bg="light" expand="lg">
        <Navbar.Brand as={Link} to="/" className="px-4">
          <img
            alt=""
            src={CodexHubLogo}
            width="120"
            height="40"
            className="d-inline-block align-top"
          />{" "}
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="basic-navbar-nav" />
        <Navbar.Collapse id="basic-navbar-nav">
          <Nav className="ms-auto">
            <Nav.Link as={Link} to="/" className="pad2horiz">
              Home
            </Nav.Link>{" "}
            <Nav.Link as={Link} to="/MyBooks" className="pad2horiz">
              My Books
            </Nav.Link>
            <NavDropdown
              // show={showDropdown}
              // onMouseEnter={showDropdownFunc}
              // onMouseLeave={() => setShowDropdown(false)}
              title="Browse"
              id="basic-nav-dropdown"
            >
              <NavDropdown.Item as={Link} to="/AdminTable">
                Recommendations
              </NavDropdown.Item>
              <NavDropdown.Item as={Link} to="#action/3.1">
                Lists
              </NavDropdown.Item>{" "}
              <NavDropdown.Item as={Link} to="#action/3.1">
                Filter
              </NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item href="#action/3.4">
                Separated link
              </NavDropdown.Item>
            </NavDropdown>
            <Form className="d-flex pad2horiz" onSubmit={() => GoHome()}>
              <Form.Control type="text" placeholder="Search books" />
              <Button type="submit">
                <i className="fas fa-search"></i>
              </Button>
            </Form>
            {permissionRole == "" ? (
              <>
                <Nav.Link as={Link} to="/SignUp" className="pad2horiz">
                  Join
                </Nav.Link>
                <Nav.Link as={Link} to="/SignIn" className="pad2horiz">
                  Login
                </Nav.Link>{" "}
              </>
            ) : (
              <>
                <Nav.Link as={Link} to="/UserProfile" className="me-3">
                  <i className="fas fa-user"></i>
                </Nav.Link>{" "}
              </>
            )}
          </Nav>
        </Navbar.Collapse>
      </Navbar>
    </div>
  );
}

export default MyNavBar;
