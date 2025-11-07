
import React, { useState, useEffect } from 'react';
import { TextField, Button, Card, CardContent, Typography } from '@mui/material';
import { createStudent, updateStudent } from '../../services/studentservices';
import type { Student } from '../../models/Student';

interface Props {
  studentToEdit?: Student;
  onSave: () => void;
}

const StudentForm: React.FC<Props> = ({ studentToEdit, onSave }) => {
  const [student, setStudent] = useState<Student>({ name: '', age: 0 });

  useEffect(() => {
    if (studentToEdit) setStudent(studentToEdit);
  }, [studentToEdit]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (student.id) {
      updateStudent(student).then(onSave);
    } else {
      createStudent(student).then(() => {
        setStudent({ name: '', age: 0 });
        onSave();
      });
    }
  };

  return (
    <Card sx={{ maxWidth: 400, margin: 'auto', mt: 4 }}>
      <CardContent>
        <Typography variant="h6">{student.id ? 'Edit Student' : 'Add Student'}</Typography>
        <form onSubmit={handleSubmit}>
          <TextField
            fullWidth
            label="Name"
            value={student.name}
            onChange={e => setStudent({ ...student, name: e.target.value })}
            margin="normal"
          />
          <TextField
            fullWidth
            label="Age"
            type="number"
            value={student.age}
            onChange={e => setStudent({ ...student, age: parseInt(e.target.value) })}
            margin="normal"
          />
          <Button type="submit" variant="contained" color="primary" sx={{ mt: 2 }}>
            {student.id ? 'Update' : 'Add'}
          </Button>
        </form>
      </CardContent>
    </Card>
  );
};

export default StudentForm;