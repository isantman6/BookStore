// Here we will create react component for generate form with validation

//React component for input component
var MyInput = React.createClass({
    //onchange event
    handleChange: function (e) {
        this.props.onChange(e.target.value);
        var isValidField = this.isValid(e.target);
    },
    //validation function
    isValid: function (input) {
        //check required field
        if (input.getAttribute('required') != null && input.value === "") {
            input.classList.add('error'); //add class error
            input.nextSibling.textContent = this.props.messageRequired; // show error message
            return false;
        }
        else {
            input.classList.remove('error');
            input.nextSibling.textContent = "";
        }
        //check data type // here I will show you email validation // we can add more and will later
        if (input.getAttribute('type') == "email" && input.value != "") {
            if (!this.validateEmail(input.value)) {
                input.classList.add('error');
                input.nextSibling.textContent = this.props.messageEmail;
                return false;
            }
            else {
                input.classList.remove('error');
                input.nextSibling.textContent = "";
            }
        }
        return true;
    },
    //email validation function
    validateEmail: function (value) {
        var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        return re.test(value);
    },
    componentDidMount: function () {
        if (this.props.onComponentMounted) {
            this.props.onComponentMounted(this); //register this input in the form
        }
    },
    //render
    render: function () {
        var inputField;
        if (this.props.type == 'textarea') {
            inputField = <textarea value={this.props.value} ref={this.props.name} name={this.props.name}
                className='form-control' required={this.props.isrequired} onChange={this.handleChange} />
        }
        else {
            inputField = <input type={this.props.type} value={this.props.value} ref={this.props.name} name={this.props.name}
                className='form-control' required={this.props.isrequired} onChange={this.handleChange} />
        }
        return (
            <div className="form-group">
                <label htmlFor={this.props.htmlFor}>{this.props.label}:</label>
                {inputField}
                <span className="error"></span>
            </div>
        );
    }
});
//React component for generate form