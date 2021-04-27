import React, { useState, useEffect } from 'react';
import { Table, Modal, Button, Form, Container, Row, Col } from 'react-bootstrap';

function ListSynonyms() {

    let word, synonym;
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    const [items, setItems] = useState([]);

    const loadData = () => {
        fetch("https://localhost:5001/synonyms/list")
          .then(res => res.json())
          .then(
            (result) => {
              setItems(result);
            });
    };

    const handlerSave = () => { 

        if (word !== undefined && synonym !== undefined) {
            fetch(`https://localhost:5001/synonyms/synonym/${word}/${synonym}`, { method: 'POST' })
            .then((result) => { loadData(); setShow(false);});            
        }

     };

    useEffect(() => {
        loadData();
    }, [])

    return (
        <Container>
        <Row>
            <Col>           
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
            <Table responsive>
                <thead>
                    <tr>
                    <th>Word</th>
                    <th>Synonyms</th>
                    </tr>
                </thead>
                {Object.keys(items).map((item) => (                
                <tbody>
                    <tr>
                    <td>{item}</td>
                    <td>{Object.keys(items[item]).map((x) => (items[item][x] + " "))}</td>
                    </tr>        
                </tbody>
                        
                ))} 
        </Table> 
        <Button variant="primary" onClick={handleShow}>Add New</Button>
            </Col>
        </Row>
        </Container>            
    );
}

export default ListSynonyms;