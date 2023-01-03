import { useEffect, useState } from "react";
// Importera VehicleItem som då representerar en rad per bil...
import CourseItem from "./CourseItem";

// Skapa komponenten VehicleList
// Container för alla våra bilar i tabell format...
function CourseList() {
  const [courses, setCourses] = useState([]);
  const [categoryFilter, setCategoryFilter] = useState("");

  // useEffect körs varje gång som en ändring sker till Virtual DOM.
  // Vi kan ange en array [] med beroenden som måste starta useEffect...
  useEffect(() => {
    loadCourses();
  }, []);

  useEffect(() => {
    loadCoursesByCategory(categoryFilter);
  });
  

  const handleCategoryFilter = (e) => {
    setCategoryFilter(e.target.value);
    loadCoursesByCategory();
  };

  const loadCoursesByCategory = async (category) => {
    if (category === "") {
      loadCourses();
      return;
    }
    const token = JSON.parse(localStorage.getItem("token"));
    console.log(token);

    const url = `${process.env.REACT_APP_BASEURL}/courses/category/${categoryFilter}`;
    const response = await fetch(url, {
      method: "GET",
      headers: {
        Authorization: `bearer ${token}`,
      },
    });

    if (!response.ok) {
      console.log("Something went wrong!");
    } else {
      setCourses(await response.json());
    }
  };

  const loadCourses = async () => {
    const token = JSON.parse(localStorage.getItem("token"));
    console.log(token);

    const url = `${process.env.REACT_APP_BASEURL}/courses/list`;
    const response = await fetch(url, {
      method: "GET",
      headers: {
        Authorization: `bearer ${token}`,
      },
    });

    if (!response.ok) {
      console.log("Something went wrong!");
    } else {
      setCourses(await response.json());
    }
  };

  const deleteCourse = async (id) => {
    console.log(`Remove the course with id ${id}`);
    const url = `${process.env.REACT_APP_BASEURL}/Courses/${id}`;
    const response = await fetch(url, {
      method: "DELETE",
    });

    console.log(response);

    if (response.status >= 200 && response.status <= 299) {
      console.log("Course removed");
      loadCourses();
    } else {
      console.log("Somthing went wrong!");
    }
  };

  return (
    <table>
      <thead>
        <tr>
          <th></th>
          <th>CourseNumber</th>
          <th>Title</th>
          <th>CourseLength</th>
          <th>
            Category
            <select onChange={handleCategoryFilter}>
              <option value="">All</option>
              <option value="C# programming">C# programming</option>
              <option value="Python">Python</option>
              <option value="Math">Math</option>
            </select>
          </th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {courses.map((course) => (
          <CourseItem
            course={course}
            key={course.courseId}
            handleDeleteCourse={deleteCourse}
            showDetails={course.showDetails}
            toggleDetails={course.toggleDetails}
          />
        ))}
      </tbody>
    </table>
  );
}

export default CourseList;
