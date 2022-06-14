import { useNavigate } from 'react-router-dom';

function CourseItem({ course, handleDeleteCourse }) {
  const navigate = useNavigate();

  const onEditClickHandler = () => {
    navigate(`/edit/${course.courseId}`);
  };

  const onDeleteClickHandler = () => {    
    handleDeleteCourse(course.courseId);
  };

  return (
    <tr>
      <td>
        <span onClick={onEditClickHandler}>
          <i className='fa-solid fa-pencil edit'></i>
        </span>
      </td>
      <td>{course.title}</td>
      <td>{course.courseNumber}</td>
      <td>{course.courseLength}</td>
      <td>{course.category}</td>
      <td>{course.description}</td>
      <td>{course.details}</td>
      <td>
        <span onClick={onDeleteClickHandler}>
          <i className='fa-solid fa-trash-can delete'></i>
        </span>
      </td>
    </tr>
  );
}

export default CourseItem;
