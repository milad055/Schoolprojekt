import { useState } from 'react';

function AddTeacher() {
    const [firstname, setFirstName] = useState('');
    const [lastname, setLastName] = useState('');
    const [email, setEmail] = useState('');
    const [telefonNumber, setTelefonNumber] = useState('');
    const [address, setAddress] = useState('');
    const [expertness, setExpertness] = useState('');

   
      const onHandleFirstNameTextChanged = (e) => {
        setFirstName(e.target.value);
      };
      const onHandleLastNameTextChanged = (e) => {
        setLastName(e.target.value);
      };
      const onHandleEmailTextChanged = (e) => {
        setEmail(e.target.value);
      };
      const onHandleTelefonNumberTextChanged = (e) => {
        setTelefonNumber(e.target.value);
      };
      const onHandleAddressTextChanged = (e) => {
        setAddress(e.target.value);
      };
      const onHandleExpertnessTextChanged = (e) => {
        setExpertness(e.target.value);
      };

      const handleSaveTeacher = (e) => {
        e.preventDefault();
        const teacher = {
            firstname,
            lastname,
            email,
            telefonNumber,
            address,
            expertness
        };
    
        console.log(teacher);
    
        saveTeacher(teacher);
      };

      const saveTeacher = async (teacher) => {
        const url = `${process.env.REACT_APP_BASEURL}/teachers`;
        const response = await fetch(url, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(teacher),
        });
    
        console.log(response);
    
        if (response.status >= 200 && response.status <= 299) {
          console.log('teacher is saved');
        } else {
          console.log('Something went wrong');
        }
      };

      return (
        <>
          <h1 className='page-title'>Add teacher</h1>
          <section className='form-container'>
            <h4>Teacher information</h4>
            <section className='form-wrapper'>
              <form className='form' onSubmit={handleSaveTeacher}>
                <div className='form-control'>
                  <label htmlFor=''>FirstName</label>
                  <input
                    onChange={onHandleFirstNameTextChanged}
                    value={firstname}
                    type='text'
                    id='firstname'
                    name='firstname'
                  />
                </div>
                <div className='form-control'>
                  <label htmlFor=''>LastName</label>
                  <input
                    onChange={onHandleLastNameTextChanged}
                    value={lastname}
                    type='text'
                    id='lastname'
                    name='lastname'
                  />
                </div>
                <div className='form-control'>
                  <label htmlFor=''>Email</label>
                  <input
                    onChange={onHandleEmailTextChanged}
                    value={email}
                    type='text'
                    id='email'
                    name='email'
                  />
                </div>
                <div className='form-control'>
                  <label htmlFor=''>TelefonNumber</label>
                  <input
                    onChange={onHandleTelefonNumberTextChanged}
                    value={telefonNumber}
                    type="text"
                    id='telefonNumber'
                    name='telefonNumber'
                  />
                </div>
                <div className='form-control'>
                  <label htmlFor=''>Address</label>
                  <input
                    onChange={onHandleAddressTextChanged}
                    value={address}
                    ype='text'
                    id='address'
                    name='address'
                  />
                </div>
                <div className='form-control'>
                  <label htmlFor=''>Expertness</label>
                  <input
                    onChange={onHandleExpertnessTextChanged}
                    value={expertness}
                    ype='text'
                    id='expertness'
                    name='expertness'
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
    
    export default AddTeacher;
    
    