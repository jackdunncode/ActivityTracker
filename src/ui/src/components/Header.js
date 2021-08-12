import { NavLink } from 'react-router-dom';

function Header() {
  return (
    <header className="d-flex flex-wrap justify-content-center py-3 mb-4 border-bottom">
      <a
        href="/"
        className="d-flex align-items-center mb-3 mb-md-0 me-md-auto text-dark text-decoration-none"
      >
        <span className="fs-4">Activity Tracker</span>
      </a>
      <ul className="nav nav-pills">
        <li className="nav-item">
          <NavLink exact to="/" className="nav-link" activeClassName="active">
            Home
          </NavLink>
        </li>
        <li className="nav-item">
          <NavLink
            exact
            to="/about"
            className="nav-link"
            activeClassName="active"
          >
            About
          </NavLink>
        </li>
      </ul>
    </header>
  );
}

export default Header;
