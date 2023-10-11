use serde::Deserialize;
use crate::{
    ticket::TicketData,
    customer::CustomerData
};

/// Holds receipt data.
#[derive(Deserialize)]
pub struct ReceiptData {
    /// Ticket id.
    pub id: i32,
    /// Ticket message.
    pub message: String,
    /// Customer id.
    #[serde(rename = "customerId")]
    pub customer_id: i32,
    /// Ticket id.
    #[serde(rename = "ticketId")]
    pub ticket_id: i32,
    /// Number of tickets created for the customer.
    #[serde(rename = "customerTicketCount")]
    pub customer_ticket_count: i32,
    /// Date and time when created in ISO-8601 format.
    #[serde(rename = "createdAt")]
    pub created_at: String,
}

/// Holds more data about receipt.
#[derive(Deserialize)]
pub struct ReceiptVerboseData {
    pub receipt: ReceiptData,
    pub ticket: TicketData,
    pub customer: CustomerData,
}
