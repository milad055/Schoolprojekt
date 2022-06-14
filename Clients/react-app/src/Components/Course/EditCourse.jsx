import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

function EditCourse() {
  const params = useParams();
  const [courseId, setCourseId] = useState('');
  const [title, setTitle] = useState('');
  const [courseNumber, setCourseNumber] = useState('');
  const [courseLength, setCourseLength] = useState('');
  const [category, setCategory] = useState('');
  const [description, setDescription] = useState('');
  const [details, setDetails] = useState('');
 

  useEffect(() => {
    fetchCourse(params.id);
  }, [params.id]);

  const fetchCourse = async (id) => {
    const url = `${process.env.REACT_APP_BASEURL}/courses/${id}`;

    const response = await fetch(url);

    if (!response.ok) {
      console.log('Could not find any course, or something went wrong');
    }

    const course = await response.json();
    console.log(course);
    setCourseId(course.courseId);
    setTitle(course.title);
    setCourseNumber(course.courseNumber);
    setCourseLength(course.courseLength);
    setCategory(course.category);
    setDescription(course.description);
    setDetails(course.details);
  };

  const onHandleCourseIdTextChanged = (e) => {
    setCourseId(e.target.value);
  };

  const onHandleTitleTextChanged = (e) => {
    setTitle(e.target.value);
  };
  const onHandleCourseNumberTextChanged = (e) => {
    setCourseNumber(e.target.value);
  };
  const onHandleCourseLengthTextChanged = (e) => {
    setCourseLength(e.target.value);
  };
  const onHandleCategoryTextChanged = (e) => {
    setCategory(e.target.value);
  };
  const onHandleDescriptionTextChanged = (e) => {
    setDescription(e.target.value);
  };
  const onHandleDetailsTextChanged = (e) => {
    setDetails(e.target.value);
  };

  const handleSaveCourse = (e) => {
    e.preventDefault();
    const course = {
        title,
        courseNumber,
        courseLength,
        category,
        description,
        details
     
    };

    console.log(course);

    saveCourse(course);
  };

  const saveCourse = async (course) => {
    const url = `${process.env.REACT_APP_BASEURL}/courses/${courseId}`;
    const response = await fetch(url, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(course),
    });

    console.log(response);

    if (response.status >= 200 && response.status <= 299) {
      console.log('Course is saved');
    } else {
      console.log('Something went wrong');
    }
  };

  return (
    <>
      <h1 className='page-title'>Update course</h1>
      <section className='form-container'>
        <h4>Course info</h4>
        <section className='form-wrapper'>
          <form className='form' onSubmit={handleSaveCourse}>
            <input
              onChange={onHandleCourseIdTextChanged}
              value={courseId}
              type='hidden'
              id='courseId'
              name='courseId'
            />
            <div className='form-control'>
              <label htmlFor=''>Title</label>
              <input
                onChange={onHandleTitleTextChanged}
                value={title}
                type='text'
                id='title'
                name='title'
              />
            </div>
            <div className='form-control'>
              <label htmlFor=''>Coursenumber</label>
              <input
                onChange={onHandleCourseNumberTextChanged}
                value={courseNumber}
                type='text'
                id='courseNumber'
                name='CourseNumber'
              />
            </div>
            <div className='form-control'>
              <label htmlFor=''>Courselength</label>
              <input
                onChange={onHandleCourseLengthTextChanged}
                value={courseLength}
                type='text'
                id='courseLength'
                name='courseLength'
              />
            </div>
            <div className='form-control'>
              <label htmlFor=''>Category</label>
              <input
                onChange={onHandleCategoryTextChanged}
                value={category}
                type='text'
                id='category'
                name='category'
              />
            </div>
            <div className='form-control'>
              <label htmlFor=''>Description</label>
              <input
                onChange={onHandleDescriptionTextChanged}
                value={description}
                ype='text'
                id='description'
                name='description'
              />
            </div>
            <div className='form-control'>
              <label htmlFor=''>Details</label>
              <input
                onChange={onHandleDetailsTextChanged}
                value={details}
                type='text'
                id='details'
                name='details'
              />
            </div>
           
            <div className='buttons'>
              <button type='submit' className='btn'>
                Save
              </button>
            </div>
          </form>
        </section>
      </section>
    </>
  );
}

export default EditCourse;
