import { useEffect, useState } from "react";
import CourseItem from "./CourseItem";

function CourseList() {
  const [courses, setCourses] = useState([]);
  const [categoryFilter, setCategoryFilter] = useState("");
  const [categories, setCategories] = useState([]);

  // useEffect körs varje gång som en ändring sker till Virtual DOM.
  // Vi kan ange en array [] med beroenden som måste starta useEffect...
  useEffect(() => {
    loadCourses();
  }, []);

  useEffect(() => {
    loadCoursesByCategory(categoryFilter);
  }, [categoryFilter]);

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
      const courses = await response.json();
      // Extract unique categories from the list of courses
      const categories = Array.from(new Set(courses.map((c) => c.category)));
      setCategories(categories);
      setCourses(courses);
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
              {categories.map((category) => (
                <option value={category}>{category}</option>
              ))}
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
