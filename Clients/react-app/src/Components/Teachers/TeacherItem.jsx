import { useNavigate } from 'react-router-dom';

function TeacherItem({ teacher, handleDeleteTeacher }) {
  const navigate = useNavigate();

  const onEditClickHandler = () => {
    navigate(`/editTeacher/${teacher.teacherId}`);
  };

  const onDeleteClickHandler = () => {    
    handleDeleteTeacher(teacher.teacherId);
  };

  return (
    <tr>
      <td>
        <span onClick={onEditClickHandler}>
          <i className='fa-solid fa-pencil edit'></i>
        </span>
      </td>
      <td>{teacher.firstname}</td>
      <td>{teacher.lastname}</td>
      <td>{teacher.email}</td>
      <td>{teacher.telefonNumber}</td>
      <td>{teacher.address}</td>
      <td>{teacher.expertness}</td>
      <td>
        <span onClick={onDeleteClickHandler}>
          <i className='fa-solid fa-trash-can delete'></i>
        </span>
      </td>
    </tr>
  );
}

export default TeacherItem;
