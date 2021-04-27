import React, { useState } from 'react';
import Modal from 'react-bootstrap/Modal';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';

function AddSynonyms() {
    
    let word, synonym;
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const handlerSave = () => { 

        if (word !== undefined && synonym !== undefined) {
            fetch(`https://localhost:5001/synonyms/synonym/${word}/${synonym}`, { method: 'POST' })
            .then((result) => { debugger; setShow(false);});            
        }

     };

    return (
        <div>
            <Button variant="primary" onClick={handleShow}>Add New</Button>
            <Button variant="primary" onClick={handleShow}>Add New</Button>
            <Form>
            <Modal show={show} onHide={handleClose}>
                <Modal.Header closeButton>
                <Modal.Title>Add New Synonym</Modal.Title>
                </Modal.Header>
                <Modal.Body>                    
                    <Form.Group controlId="formWord">
                        <Form.Label>Word</Form.Label>
                        <Form.Control type="text" placeholder="" onChange={(e) => { word = e.target.value; }} />
                    </Form.Group>
                    <Form.Group controlId="formSynonym">
                        <Form.Label>Synonym</Form.Label>
                        <Form.Control type="text" placeholder="" onChange={(e) => { synonym = e.target.value; }}/>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>Close</Button>
                <Button variant="primary" onClick={handlerSave}>Save</Button>
                </Modal.Footer>
            </Modal>
            </Form>
        </div>
    );
}


export default AddSynonyms;