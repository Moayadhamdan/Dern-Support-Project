import React from "react";
import { Link } from "react-router-dom";
import "./Footer.css"; // Import the CSS file

function Footer() {
  return (
    <footer className="footer">
      <div className="footer__content">
        <div className="footer__section">
          <h4>Quick Links</h4>
          <ul>
            <li><Link to="/privacy-policy">Privacy Policy</Link></li>
            <li><Link to="/terms-of-service">Terms of Service</Link></li>
            <li><Link to="/faqs">FAQs</Link></li>
            <li><Link to="/contact">Contact Us</Link></li>
          </ul>
        </div>

        <div className="footer__section">
          <h4>Contact Us</h4>
          <p>Email: support@dern-support.com</p>
          <p>Phone: +1 (555) 123-4567</p>
          <p>Address: 123 Tech Street, IT City</p>
        </div>

        <div className="footer__section">
          <h4>Follow Us</h4>
          <ul className="footer__social-media">
            <li><a href="https://facebook.com" target="_blank" rel="noreferrer">Facebook</a></li>
            <li><a href="https://twitter.com" target="_blank" rel="noreferrer">Twitter</a></li>
            <li><a href="https://linkedin.com" target="_blank" rel="noreferrer">LinkedIn</a></li>
          </ul>
        </div>
      </div>
      <div className="footer__bottom">
        <p>© 2024 Dern-Support. All rights reserved.</p>
      </div>
    </footer>
  );
}

export default Footer;
