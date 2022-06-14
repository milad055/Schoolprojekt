// Reacts navigerings motor...
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';

// Importera kompenenten Navbar...
import Navbar from './Components/navbar/Navbar';
// Importera komponenten Home...
import Home from './Components/home/Home';

// Importera komponenter
import CourseList from './Components/Course/CourseList';
import EditCourse from './Components/Course/EditCourse';
import AddCourse from './Components/Course/AddCourse';

import StudentList from './Components/Students/StudentList';
import AddStudent from './Components/Students/AddStudent';
import EditStudent from './Components/Students/EditStudent';

import TeacherList from './Components/Teachers/TeacherList';
import AddTeacher from './Components/Teachers/AddTeacher';
import EditTeacher from './Components/Teachers/EditTeacher';

// Importera Login komponenten...
import Login from './Components/authentication/Login';

// Importera huvud css filerna...
import './utilities.css';
import './style.css';

function App() {
    // Steg 2. returnera JSX(html med eventuella dynamiska skript...)
    return (
        <Router>
          <Navbar />
          <main>
            <Routes>
              <Route path='/' element={<Home />} />
              <Route path='/list' element={<CourseList />} />
              <Route path='/edit/:id' element={<EditCourse />} />
              <Route path='/add' element={<AddCourse />} />
              <Route path='/liststudents' element={<StudentList />} />
              <Route path='/addStudent' element={<AddStudent />} />
              <Route path='/editStudent/:id' element={<EditStudent />} />
              <Route path='/listteachers' element={<TeacherList />} />
              <Route path='/addteacher' element={<AddTeacher />} />
              <Route path='/editTeacher/:id' element={<EditTeacher />} />
              <Route path='/login' element={<Login />} />
            </Routes>
          </main>
        </Router>
      );
}

export default App;
