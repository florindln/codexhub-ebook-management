import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import "./NavBar.css";
import Menu from "@material-ui/core/Menu";
import MenuItem from "@material-ui/core/MenuItem";

function NavBar() {
  const [button, setButton] = useState(true);
  const [click, setClick] = useState(false);
  const [showDetails, setShowDetails] = useState(false);
  const [showAdmin, setShowAdmin] = useState(false);
  const [showUser, setShowUser] = useState(false);
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [anchorEl2, setAnchorEl2] = React.useState(null);
  const [anchorEl3, setAnchorEl3] = React.useState(null);

  const handleMenuClick = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };
  const handleMenu2Click = (event) => {
    setAnchorEl2(event.currentTarget);
  };

  const handleMenu2Close = () => {
    setAnchorEl2(null);
  };
  const handleMenu3Click = (event) => {
    setAnchorEl3(event.currentTarget);
  };

  const handleMenu3Close = () => {
    setAnchorEl3(null);
  };

  const handleClick = () => setClick(!click);
  const closeMobileMenu = () => setClick(false);
  const handleShowDetails = () => setShowDetails(!showDetails);

  // const signOut = () => {
  //   AuthenticationService.signOut();
  //   window.location.reload();
  // };

  const showButton = () => {
    if (window.innerWidth <= 960) {
      setButton(false);
    } else {
      setButton(true);
    }
  };

  useEffect(() => {
    // const role = AuthenticationService.getCurrentUserRole();
    const role = "User";
    // console.log(role)
    if (role === "User") {
      setShowUser(true);
    }
    if (role === "Admin") {
      setShowAdmin(true);
      setShowUser(true);
    }
  }, []);

  window.addEventListener("resize", showButton);
  useEffect(() => {
    showButton();
  }, []);

  return (
    <div>
      <nav className="myNav">
        <Link to="/" className="navbar-logo" onClick={closeMobileMenu}>
          BSky
          <i class="fab fa-xing"></i>
        </Link>
        <div className="menu-icon" onClick={handleClick}>
          <i className={click ? "fas fa-times" : "fas fa-bars"}></i>
        </div>
        <div className="navbar-container">
          <ul className={click ? "nav-menu active" : "nav-menu"}>
            <li className="nav-item">
              <Link to="/" className="nav-links" onClick={closeMobileMenu}>
                Home
              </Link>
            </li>
            <li className="nav-item">
              {showAdmin ? (
                <div className="nav-links">
                  <div
                    style={{ cursor: "pointer" }}
                    aria-controls="simple-menu"
                    aria-haspopup="true"
                    onMouseEnter={handleMenu3Click}
                    onClick={handleMenu3Click}
                  >
                    <div> Airplanes</div>
                  </div>
                  <Menu
                    id="simple-menu"
                    anchorEl={anchorEl3}
                    keepMounted
                    open={Boolean(anchorEl3)}
                    onClose={handleMenu3Close}
                    MenuListProps={{ onMouseLeave: handleMenu3Close }}
                  >
                    <MenuItem onClick={handleMenu3Close}>
                      <Link to="/airplanes" onClick={closeMobileMenu}>
                        Our planes
                      </Link>
                    </MenuItem>
                    <MenuItem onClick={handleMenu3Close}>
                      <Link to="/airplane/admin" onClick={closeMobileMenu}>
                        Planes Overview
                      </Link>
                    </MenuItem>
                  </Menu>
                </div>
              ) : (
                <Link
                  to="/airplanes"
                  className="nav-links"
                  onClick={closeMobileMenu}
                >
                  Our planes
                </Link>
              )}
            </li>
            {/* <li className="nav-item">
              <Link to="/" className="nav-links" onClick={closeMobileMenu}>
                page
              </Link>
            </li> */}
            <li className="nav-item">
              {showAdmin ? (
                <div className="nav-links">
                  <div
                    style={{ cursor: "pointer" }}
                    aria-controls="simple-menu"
                    aria-haspopup="true"
                    onClick={handleMenuClick}
                    onMouseEnter={handleMenuClick}
                  >
                    <div> Flights</div>
                  </div>
                  <Menu
                    id="simple-menu"
                    anchorEl={anchorEl}
                    keepMounted
                    open={Boolean(anchorEl)}
                    onClose={handleMenuClose}
                    MenuListProps={{ onMouseLeave: handleMenuClose }}
                  >
                    <MenuItem onClick={handleMenuClose}>
                      <a
                        style={{ cursor: "pointer" }}
                        onClick={(closeMobileMenu, handleShowDetails)}
                      >
                        Check Flights
                      </a>
                    </MenuItem>
                    <MenuItem onClick={handleMenuClose}>
                      <Link to="/flight/admin" onClick={closeMobileMenu}>
                        Flights Overview
                      </Link>
                    </MenuItem>
                  </Menu>
                </div>
              ) : (
                <a
                  style={{ cursor: "pointer" }}
                  className="nav-links"
                  onClick={(closeMobileMenu, handleShowDetails)}
                >
                  Check Flights
                </a>
              )}
            </li>
            {showDetails && <div className="toPopup">"component here"</div>}
            <li className="nav-item">
              {showAdmin ? (
                <div className="nav-links">
                  <div
                    style={{ cursor: "pointer" }}
                    aria-controls="simple-menu"
                    aria-haspopup="true"
                    onClick={handleMenu2Click}
                    onMouseEnter={handleMenu2Click}
                  >
                    <div> Users</div>
                  </div>
                  <Menu
                    id="simple-menu"
                    anchorEl={anchorEl2}
                    keepMounted
                    open={Boolean(anchorEl2)}
                    onClose={handleMenu2Close}
                    MenuListProps={{ onMouseLeave: handleMenu2Close }}
                  >
                    <MenuItem onClick={handleMenu2Close}>
                      <Link to="/profile" onClick={closeMobileMenu}>
                        Profile
                      </Link>
                    </MenuItem>
                    <MenuItem onClick={handleMenu2Close}>
                      <Link to="/userOverview" onClick={closeMobileMenu}>
                        User Overview
                      </Link>
                    </MenuItem>
                  </Menu>
                </div>
              ) : (
                showUser && (
                  <Link
                    to="/profile"
                    className="nav-links"
                    onClick={closeMobileMenu}
                  >
                    Profile
                  </Link>
                )
              )}
            </li>
            <li className="nav-item">
              {showUser ? (
                <Link
                  to="/"
                  className="nav-links"
                  onClick={() => {
                    closeMobileMenu();
                    // signOut();
                  }}
                >
                  Sign Out
                </Link>
              ) : (
                <Link
                  to="/login"
                  className="nav-links"
                  onClick={() => {
                    closeMobileMenu();
                  }}
                >
                  Sign In
                </Link>
              )}
            </li>
          </ul>
        </div>
      </nav>
    </div>
  );
}

export default NavBar;
