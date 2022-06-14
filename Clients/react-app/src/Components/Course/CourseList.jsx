import { useEffect, useState } from 'react';
// Importera VehicleItem som då representerar en rad per bil...
import CourseItem from './CourseItem';

// Skapa komponenten VehicleList
// Container för alla våra bilar i tabell format...
function CourseList() {
  const [courses, setCourses] = useState([]);

  // useEffect körs varje gång som en ändring sker till Virtual DOM.
  // Vi kan ange en array [] med beroenden som måste starta useEffect...
  useEffect(() => {
    loadCourses();
  }, []);

  const loadCourses = async () => {
    const token = JSON.parse(localStorage.getItem('token'));
    console.log(token);

    const url = `${process.env.REACT_APP_BASEURL}/courses/list`;
    const response = await fetch(url, {
      method: 'GET',
      headers: {
        Authorization: `bearer ${token}`,
      },
    });

    if (!response.ok) {
      console.log('Something went wrong!');
    } else {
      setCourses(await response.json());
    }
  };

  const deleteCourse = async (id) => {
    console.log(`Remove the course with id ${id}`);
    const url = `${process.env.REACT_APP_BASEURL}/Courses/${id}`;
    const response = await fetch(url, {
      method: 'DELETE',
    });

    console.log(response);

    if (response.status >= 200 && response.status <= 299) {
      console.log('Course removed');
      loadCourses();
    } else {
      console.log('Somthing went wrong!');
    }
  };

  return (
    <table>
      <thead>
        <tr>
          <th></th>
          <th>Title</th>
          <th>CourseNumber</th>
          <th>CourseLength</th>
          <th>Category</th>
          <th>Description</th>
          <th>Details</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {courses.map((course) => (
          <CourseItem
            course={course}
            key={course.courseId}
            handleDeleteCourse={deleteCourse}
          />
        ))}
      </tbody>
    </table>
  );
}

export default CourseList;
