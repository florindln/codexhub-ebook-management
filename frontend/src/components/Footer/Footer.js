import React from "react";
import "./Footer.css";
import { Link } from "react-router-dom";

function Footer() {
  return (
    <div className="completeFooter">
      <div className="footer-container">
        <section className="footer-subscription">
          <p className="footer-subscription-heading">
            Join our newsletter for the latest offers
          </p>
          <p className="footer-subscription-text">
            You can unsubscribe at any time.
          </p>
          <div className="input-areas">
            <form>
              <input
                className="footer-input"
                name="email"
                type="email"
                placeholder="Your Email"
              />
              <button type="submit" class="btn btn-outline-primary">
                Subscribe
              </button>
            </form>
          </div>
        </section>
        <div class="footer-links">
          <div className="footer-link-wrapper">
            <div class="footer-link-items">
              <h2>About Us</h2>
              <Link to="/sign-up">Business goal</Link>
              <Link to="/">Testimonials</Link>
              <Link to="/">Careers</Link>
              <Link to="/">Investors</Link>
              <Link to="/">Terms of Service</Link>
            </div>
            <div class="footer-link-items">
              <h2>Contact Us</h2>
              <Link to="/">Contact</Link>
              <Link to="/">Destinations</Link>
              <Link to="/">Support</Link>
              <Link to="/">Business sponsorships</Link>
            </div>
          </div>
        </div>
        <section class="social-media">
          <div class="social-media-wrap">
            <div class="footer-logo">
              <Link to="/" className="social-logo">
                BSky
                <i class="fab fa-xing"></i>
              </Link>
            </div>
            <small class="website-rights">BSky © 2021</small>
            <div class="social-icons">
              <a
                class="social-icon-link facebook"
                href="https://www.facebook.com/bsky"
                target="_blank"
                aria-label="Facebook"
              >
                <i class="fab fa-facebook-f" />
              </a>
              <a
                class="social-icon-link instagram"
                href="https://www.instagram.com/bsky"
                target="_blank"
                aria-label="Instagram"
              >
                <i class="fab fa-instagram" />
              </a>
              <a
                class="social-icon-link youtube"
                href="https://www.youtube.com/bsky"
                target="_blank"
                aria-label="Youtube"
              >
                <i class="fab fa-youtube" />
              </a>
              <a
                class="social-icon-link twitter"
                href="https://www.twitter.com/bsky"
                target="_blank"
                aria-label="Twitter"
              >
                <i class="fab fa-twitter" />
              </a>
              <a
                class="social-icon-link twitter"
                href="https://www.linkedin.com/bsky"
                target="_blank"
                aria-label="LinkedIn"
              >
                <i class="fab fa-linkedin" />
              </a>
            </div>
          </div>
        </section>
      </div>
    </div>
  );
}

export default Footer;
