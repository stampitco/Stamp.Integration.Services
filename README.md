# Introduction

This repository contains a library `Stamp.Integration.Services` which can be used to communicate with Stamp Interation APIs, and an example `TestConsole` which shows how `Stamp.Integration.Services` can be used.

Stamp.Integration.Services
-----

This project contains two classes `StampMerchantService` and `StampInvoicingService` which provides a wrapper for the APIs described in [Stamp Integration APIs.pdf](https://github.com/stampitco/Stamp.Integration.Services/blob/develop/Stamp%20Integration%20APIs.pdf).

TestConsole
-----

This project shows how to use `Stamp.Integration.Services` and executes the following flow:

1) Get an access token
2) Upload an invoice draft
3) Open Stamp Merchant Portal to search the customer and create the invoice
4) Get invoiced customer
5) Get invoice status
6) Get Otello communication id
5) Cancel an invoice
6) Open Stamp Merchant Portal to check invoice status
