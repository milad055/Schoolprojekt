import { NavLink } from 'react-router-dom';

function Navbar() {
  return (
    <nav id='navbar'>
      <h1 className='logo'>
        <span className='text-primary'>
          <i className='fa-solid fa-book'></i> School
        </span>
        App
      </h1>
      <ul>
        <li>
          <NavLink to='/'>Start</NavLink>
          <NavLink to='/list'>Courses</NavLink>
          <NavLink to='/add'>Add Course</NavLink>
          <NavLink to='/liststudents'>Students</NavLink>
          <NavLink to='/addStudent'>Add Student</NavLink>
          <NavLink to='/listteachers'>Teachers</NavLink>
          <NavLink to='/addteacher'>Add Teachers</NavLink>
          <NavLink to='/login'>Login</NavLink>
        </li>
      </ul>
    </nav>
  );
}

export default Navbar;
