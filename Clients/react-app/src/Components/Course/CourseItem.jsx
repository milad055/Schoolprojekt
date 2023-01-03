import { useNavigate } from "react-router-dom";
import { useState } from "react";

function CourseItem({ course, handleDeleteCourse }) {
  const navigate = useNavigate();
  const [showDetails, setShowDetails] = useState(false);

  const onEditClickHandler = () => {
    navigate(`/edit/${course.courseId}`);
  };

  const onDeleteClickHandler = () => {
    handleDeleteCourse(course.courseId);
  };

  const toggleDetails = () => {
    setShowDetails(!showDetails);
  };

  return (
    <tr>
      <td>
        <span onClick={onEditClickHandler}>
          <i className="fa-solid fa-pencil edit"></i>
        </span>
      </td>
      <td>{course.courseNumber}</td>
      <td>{course.title}</td>
      <td>{course.courseLength}</td>
      <td>{course.category}</td>
      <td>
        <button onClick={toggleDetails}>More Info</button>
      </td>
      {showDetails && (
        <td>
          <th>Description: {course.description}</th>
          <th>Details: {course.details}</th>
        </td>
      )}
      <td>
        <span onClick={onDeleteClickHandler}>
          <i className="fa-solid fa-trash-can delete"></i>
        </span>
      </td>
    </tr>
  );
}

export default CourseItem;
