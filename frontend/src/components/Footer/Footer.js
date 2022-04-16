import React from "react";
import "./Footer.css";
import { Link } from "react-router-dom";

function Footer() {
  return (
    <div className="completeFooter">
      <div className="footer-container">
        <div className="footer-links">
          <div className="footer-link-wrapper">
            <div className="footer-link-items">
              {/* <h2>About Us</h2> */}
              <Link to="/sign-up">Business goal</Link>
            </div>
            <div className="footer-link-items">
              {/* <h2>Contact Us</h2> */}
              <Link to="/">Team</Link>
            </div>
            <div className="footer-link-items">
              <Link to="/">Opportunities</Link>
            </div>
            <div className="footer-link-items">
              <Link to="/">Careers</Link>
            </div>
          </div>
        </div>
        <section className="social-media">
          <div className="social-media-wrap">
            <div className="footer-logo">
              <Link to="/" className="social-logo">
                Codexhub
                {/* <i className="fab fa-xing"></i> */}
              </Link>
            </div>
            <small className="website-rights">Codexhub © 2022</small>
            <div className="social-icons">
              <a
                className="social-icon-link facebook"
                href="https://www.facebook.com/bsky"
                target="_blank"
                aria-label="Facebook"
              >
                <i className="fab fa-facebook-f" />
              </a>
              <a
                className="social-icon-link instagram"
                href="https://www.instagram.com/bsky"
                target="_blank"
                aria-label="Instagram"
              >
                <i className="fab fa-instagram" />
              </a>
              <a
                className="social-icon-link youtube"
                href="https://www.youtube.com/bsky"
                target="_blank"
                aria-label="Youtube"
              >
                <i className="fab fa-youtube" />
              </a>
              <a
                className="social-icon-link twitter"
                href="https://www.twitter.com/bsky"
                target="_blank"
                aria-label="Twitter"
              >
                <i className="fab fa-twitter" />
              </a>
              <a
                className="social-icon-link twitter"
                href="https://www.linkedin.com/bsky"
                target="_blank"
                aria-label="LinkedIn"
              >
                <i className="fab fa-linkedin" />
              </a>
            </div>
          </div>
        </section>
      </div>
    </div>
  );
}

export default Footer;
