/// Holds customer data.
pub struct CustomerData {
    /// Customer id.
    pub id: i32,
    /// Customer first name.
    pub first_name: String,
    /// Customer last name.
    pub last_name: String,
    /// Customer email address.
    pub email: String,
    /// Date and time when created in ISO-8601 format.
    pub created_at: String,
}

/// Used to create customers.
pub struct CustomerCreateData {
    /// Customer first name.
    pub first_name: String,
    /// Customer last name.
    pub last_name: String,
    /// Customer email address.
    pub email: String,
}

impl CustomerCreateData {
    pub fn new(first_name: &str, last_name: &str, email: &str) -> CustomerCreateData {
        CustomerCreateData {
            first_name: first_name.to_string(),
            last_name: last_name.to_string(),
            email: email.to_string(),
        }
    }
}